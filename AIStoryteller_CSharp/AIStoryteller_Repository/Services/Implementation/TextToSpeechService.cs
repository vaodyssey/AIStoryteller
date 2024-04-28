using AIStoryteller_Repository.Constants;
using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Repositories;
using AIStoryteller_Repository.Repositories.Implementation;
using AIStoryteller_Repository.SignalR;
using AIStoryteller_Repository.Utils;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AIStoryteller_Repository.Services.Implementation
{
    public class TextToSpeechService
    {

        private int _bookId;
        private int _convertedCount;
        private List<Page> _pages;
        private IPageRepository _pageRepository;
        private readonly IHubContext<ProgressHub> _hubContext;
        public TextToSpeechService(IPageRepository pageRepository,
            IHubContext<ProgressHub> hubContext)
        {

            _convertedCount = 0;
            _pageRepository = pageRepository;
            _hubContext = hubContext;
        }
        public async Task Convert(int bookId)
        {
            _bookId = bookId;
            await GetPagesByBookId();
            await ConvertPagesToAudio();
        }
        private async Task GetPagesByBookId()
        {
            _pages = await _pageRepository.GetMultipleBy(page => page.BookId == _bookId);
        }
        private Task ConvertPagesToAudio()
        {
            return Task.Run(() =>
            {
                List<Task> ttsTasks = new List<Task>();
                for (int pageIndex = 0; pageIndex < _pages.Count(); pageIndex++)
                {
                    var page = _pages.ElementAt(pageIndex);
                    string outputName = page.AudioPath;

                    var ttsTask = new Task(async () =>
                    {
                        var cmdInstance = await OpenHiddenCMD();
                        await ActivatePythonVirtualEnvironment(cmdInstance);
                        await TTSConversion(cmdInstance, page.Content, page.PageNumber);
                    });
                    ttsTask.Start();
                    ttsTasks.Add(ttsTask);
                }

                Task.WaitAll(ttsTasks.ToArray());
            });
        }
        private Task<Process> OpenHiddenCMD()
        {
            return Task.Run(() =>
            {
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                };
                Process process = new Process()
                {
                    StartInfo = psi,
                };

                process.Start();
                return process;

            });
        }
        private Task ActivatePythonVirtualEnvironment(Process cmdProcess)
        {
            return Task.Run(() =>
            {
                cmdProcess.StandardInput.WriteLine($@"cd /d ""{Paths.PythonProjectPath}""");
                cmdProcess.StandardInput.WriteLine($@"""{Paths.ActivatePythonVenvScriptPath}""");
            });
        }
        private async Task TTSConversion(Process cmdProcess, string text, int pageNumber)
        {                
            if (string.IsNullOrEmpty(text)) return;
            string cleanedText = TextUtils.CleanAnomaliesFromText(text);    
            string command = $@"python ""{Paths.TtsPythonScriptPath}"" ""{cleanedText}"" ""en"" ""{Paths.TtsAudioOutputPath}\\ttsOutput{pageNumber}.mp3"" ""{Paths.RvcAudioOutputPath}\\rvcOutput{pageNumber}.mp3""";
            
            cmdProcess.StandardInput.WriteLine(command);            
            cmdProcess.StandardInput.Flush();
            cmdProcess.StandardInput.Close();
            while (!cmdProcess.StandardOutput.EndOfStream)
            {
                Debug.WriteLine(cmdProcess.StandardOutput.ReadLine());                
            }
            cmdProcess.WaitForExit();
            await UpdateProgress();
        }
        private async Task UpdateProgress()
        {
            _convertedCount++;
            int progress = (int)Math.Round((double)(100 * _convertedCount) / _pages.Count);
            if (_convertedCount == _pages.Count)
                progress = 100;
            await _hubContext.Clients.All.SendAsync("ConvertProgressChanged", progress);
        }

    }


}
