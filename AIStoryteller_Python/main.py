import sys
import os
import pyttsx3
import textToSpeech
import rvc
import utils
import shutil
import traceback
text = sys.argv[1]
language = sys.argv[2]
ttsAudioOutputPath = sys.argv[3]
rvcAudioOutputPath = sys.argv[4]

    
def moveToOutputPath(src:str,dest:str,mp3Name:str):        
    srcPathWithFileName = utils.convertWindowsPathToPythonPath(f'{src}\\{mp3Name}')
    print('Current srcPathWithFileName: '+srcPathWithFileName)
    destPathWithFileName = utils.convertWindowsPathToPythonPath(f'{dest}')
    print('Current destPathWithFileName: '+destPathWithFileName)
    print('Current dest: '+dest)
    shutil.move(srcPathWithFileName,destPathWithFileName)

if __name__ == "__main__":        
    try:
        textToSpeech.execute(text,language,ttsAudioOutputPath)
        rvcAudioOutputName = rvc.execute(ttsAudioOutputPath)                        
        print(rvcAudioOutputPath)
        moveToOutputPath(os.getcwd(),rvcAudioOutputPath,rvcAudioOutputName)
    except Exception as e:      
        traceback.print_exc() 
        print(repr(e))