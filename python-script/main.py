import socket
import json

if __name__ == "__main__":
    ip = "127.0.0.1"
    port = 1234

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind((ip, port))
    server.listen(1)
    print("Listening for connections..")

    while True:
        try:
            client, address = server.accept()
            print(f"Connection received: {address[0]}:{address[1]}")
            string = client.recv(1024)
            string = string.decode("utf-8")
            print("Message = " + string)

            # Opening JSON file

            f = open(string, 'r')
            data = json.load(f)
            f.close()
            print(json.dumps(data))

            client.send(json.dumps(data).encode())

            client.close()

        except:
            print("An exception occurred")
            client.send("An error occurred".encode())
            client.close()

