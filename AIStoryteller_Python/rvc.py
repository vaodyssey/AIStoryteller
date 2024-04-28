from modules import *
from config import *
import json
import wave
import sys
from dotenv import load_dotenv
class Parameters():    
    speaker_id = None
    input_audio0 = None
    vc_transform0 = None
    f0_file = None
    f0method0 = None
    file_index1 = None
    file_index2 = None    
    index_rate1 = None
    filter_radius0 = None
    resample_sr0 = None
    rms_mix_rate0 = None
    protect0 = None
class ParametersBuilder:    
    data = None
    params = None
    def __init__(self) -> None:
        self.get_parameters_from_json()
    def get_parameters_from_json(self):
        file = open('vcconfig.json')
        self.data = json.load(file)
    def convert_data_to_parameters(self,audioPath:str):
        self.params = Parameters()
        self.params.speaker_id = self.data['speaker_id']
        self.params.input_audio0 = audioPath
        self.params.vc_transform0 = self.data['vc_transform0']
        self.params.f0_file = self.data['f0_file']
        self.params.f0method0= self.data['f0method0']
        self.params.file_index1 = self.data['file_index1']
        self.params.file_index2 = self.data['file_index2']            
        self.params.index_rate1 = self.data['index_rate1']
        self.params.filter_radius0 = self.data['filter_radius0']
        self.params.resample_sr0 = self.data['resample_sr0']
        self.params.rms_mix_rate0 = self.data['rms_mix_rate0']
        self.params.protect0 = self.data['protect0']
        return self.params

class MP3Saver():
    def bytes_to_wav(self,byte_data, filename):
        with wave.open(filename, 'wb') as wav_file:
            wav_file.setnchannels(1)
            wav_file.setsampwidth(2)
            wav_file.setframerate(44100)
            wav_file.writeframes(byte_data)
        return filename


    
class Main():
    voice_converter = None    
    def __init__(self) -> None:
        self.init_voice_converter()
    def init_voice_converter(self):
        config = Config()
        self.voice_converter = VC(config)                
    def convert(self,parameters:Parameters):
        self.voice_converter.get_vc(parameters.f0_file,parameters.protect0,0.33)
        result = self.voice_converter.vc_single(
            parameters.speaker_id,
            parameters.input_audio0,
            parameters.vc_transform0,
            parameters.f0_file,
            parameters.f0method0,
            parameters.file_index1,
            parameters.file_index2,            
            parameters.index_rate1,
            parameters.filter_radius0,
            parameters.resample_sr0,
            parameters.rms_mix_rate0,
            parameters.protect0
        )
        return result

def execute(audioPath:str)->str:
    main = Main()    
    paramsBuilder = ParametersBuilder()
    mp3Saver = MP3Saver()
    load_dotenv()

    print("current audio path:" +str(audioPath))
    parameters = paramsBuilder.convert_data_to_parameters(audioPath=str(audioPath))
    result_audio_bytes = main.convert(parameters=parameters)[1][1]
    outputAudioPath = mp3Saver.bytes_to_wav(result_audio_bytes,"result.wav")
    return outputAudioPath
   
   
    
    