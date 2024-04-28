import pyttsx3
import shutil
import os
import utils
import sys


def execute(text:str,language:str,output:str):
    print('Conversion for file '+output+' starts.')
    engine = pyttsx3.init()         
    voices = engine.getProperty('voices')  
    engine.setProperty('rate', 140)  
    engine.setProperty('voice', voices[1].id)   #changing index, changes voices. 1 for female         
    engine.save_to_file(text,output)                
    engine.runAndWait()   
    print('Conversion for file '+output+' finishes.')

