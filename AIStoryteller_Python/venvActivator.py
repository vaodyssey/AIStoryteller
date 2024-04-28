import os
import subprocess

def activateVenv():
    currentPath = os.getcwd()
    batPath = convertWindowsPathToPythonPath(rf'{currentPath}\.venv\Scripts\activate.bat')
    
    print(batPath)
    # p = Popen("activate.bat", cwd=batPath)
    # subprocess.run(["start", str(batPath)], shell=True)
    exec(open(batPath).read(), {'__file__': batPath})
    
activateVenv()
