
# Cambrian Robotics Unity Test

A simple python server which receives a file path from a Unity application. This file path points to a .json file. The python program reads the .json file and sends the data back to Unity via a TCP socket.




## How to run
1. Run the "main.py" file located in the "python-script" folder. This starts the server listening on 127.0.0.1 on port 1234.
2. Copy the full path to the "ArrayOfTransforms.json" file located in the "json-file" folder.
3. Run the "Cambrian Unity Test.exe" file inside the "unity-build" folder.
4. Paste the file path to the "ArrayOfTransforms.json" in to the text field in the top left and click the "Load File" button.