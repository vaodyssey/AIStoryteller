import sys
import pyttsx3
text = sys.argv[1]
language = sys.argv[2]
output = sys.argv[3]
if __name__ == "__main__":        
    try:
        print('Conversion for file '+output+' starts.')
        engine = pyttsx3.init()         
        voices = engine.getProperty('voices')  
        engine.setProperty('rate', 140)  
        engine.setProperty('voice', voices[1].id)   #changing index, changes voices. 1 for female         
        engine.save_to_file(text,output)                
        engine.runAndWait()   
        print('Conversion for file '+output+' finishes.')
    except Exception as e:      
        print(repr(e))