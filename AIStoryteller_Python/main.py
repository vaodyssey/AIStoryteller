from pdfReader import *
from textToSpeech import *

if __name__ == "__main__":
    text = readTextFromPdfByPage('D:\Atomic_Habits.pdf', 80)
    readAloud("Chao xìn BuBu chúi",'vi')
