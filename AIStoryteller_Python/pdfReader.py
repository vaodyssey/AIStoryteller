import fitz 

def readTextFromPdfByPage(filePath:str,pageNumber:int) -> str:
    doc = fitz.open(filePath) 
    page = doc[pageNumber]
    text=page.get_text() 
    return text