using AIStoryteller_Repository.Constants;
using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Repositories;
using AIStoryteller_Repository.SignalR;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    string outputName = $"output{pageIndex + 1}.mp3";
                    var ttsTask = new Task(() => RunTTS(page.Content, outputName));
                    ttsTask.Start();
                    ttsTasks.Add(ttsTask);
                }

                Task.WaitAll(ttsTasks.ToArray());
            });
        }
        private async void RunTTS(string text, string outputName)
        {
            if (string.IsNullOrEmpty(text)) return;
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "python",
                Arguments = $"\"{Paths.TtsPythonScriptPath}\" \"{text}\" \"en\" \"{Paths.TempAudioPath}\\{outputName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true

            };
            var process = Process.Start(psi);
            while (!process.StandardOutput.EndOfStream)
            {
                Debug.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
            await UpdateProgress();
        }
        private async Task UpdateProgress()
        {
            _convertedCount++;
            int progress = (int)Math.Round((double)(100 * _convertedCount) / _pages.Count);
            await _hubContext.Clients.All.SendAsync("ConvertProgressChanged", progress);
        }

    }


}
