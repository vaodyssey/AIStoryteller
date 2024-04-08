from pdfReader import *
from texttospeech import *

if __name__ == "__main__":
    text = readTextFromPdfByPage('D:\Atomic_Habits.pdf', 80)
    readAloud(text,'en')
