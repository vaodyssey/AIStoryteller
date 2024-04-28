import os
def convertWindowsPathToPythonPath(path):    
    newPath = path.replace(os.sep, '/')         
    return newPath