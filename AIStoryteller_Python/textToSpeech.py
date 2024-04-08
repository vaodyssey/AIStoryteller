from gtts import gTTS   
def readAloud(text:str,language:str):    
    myobj = gTTS(text=text, lang=language, slow=False)     
    myobj.save("welcome.mp3") 
  