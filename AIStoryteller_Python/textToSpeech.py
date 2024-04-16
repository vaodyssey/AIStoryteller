from gtts import gTTS   
import shutil
import os
def readAloud(text:str,language:str,outputDir:str):    
    tts = gTTS(text=text, lang=language, slow=False)  
    print("TTS Starts! ")       
    tts.save(outputDir) 
    os.wait()
    print("TTS Finishes! ")       
    
  