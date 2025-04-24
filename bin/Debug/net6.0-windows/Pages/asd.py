import subprocess
import time
import os
import pyautogui

def restart_program(command, working_dir):
    while True:
        try:
            print(f"Starting program: {command}")
            process = subprocess.Popen(command, shell=True, cwd=working_dir)
            process.wait()
        except Exception as e:
            print(f"Error: {e}")
        
        print("Program crashed, restarting in 5 seconds...")
        time.sleep(1)

while True==True:
    # Captura uma captura de tela
    screenshot = pyautogui.screenshot()
    print("screenshot")
    # Salva a captura de tela em um arquivo (opcional)
    screenshot.save('screenshot.png')

    # Localiza a imagem na captura de tela
    image_path = 'botao.png'
    region = (0, 0, 1366, 768)  # Define a região da tela onde procurar
    confidence = 0.6  # Define a confiança mínima para considerar a imagem encontrada

    try: 
        location = pyautogui.locateOnScreen(image_path, region=region, confidence=confidence)

        if location:
            print('detectado')
            # Obtém as coordenadas do centro da imagem
            x, y = pyautogui.center(location)
            # Clica no centro da imagem
            pyautogui.click(x, y)
        else:
            print("Imagem não encontrada na tela.")
    except:
        pass

if __name__ == "__main__":
    command = "C:\\Users\\Desconhecido\\Desktop\\net6.0-windows\\Printer Web Forms.exe"  # Substitua pelo seu comando
    working_dir = r"C:\Users\Desconhecido\Desktop\net6.0-windows"  # Substitua pelo diretório desejado
    restart_program(command, working_dir)


