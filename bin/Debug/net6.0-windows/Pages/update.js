window.onload = function() {
    loadClientes();
    loadTamanhos();
    loadTipos();
    loadOffsets();

    var maintitle = document.getElementById("maintitle");

    toggleButton('imp01');
    toggleButton('imp02');
    toggleButton('imp03');

    var button  = document.getElementById("imp01");
    var button2 = document.getElementById("imp02");
    var button3 = document.getElementById("imp03");
    
    button.value = "IMP " + FIRST_PRINTER
    button2.value = "IMP " + (FIRST_PRINTER+1)
    button3.value = "IMP " + (FIRST_PRINTER+2)

    maintitle.text = "Torre" + TOWER_ID;
};


function loadOffsets() {
    // Faz uma solicitação HTTP para obter os dados dos offsets 
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `http://${SERVER_IP}/offset?tower=${TOWER_ID}`, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // Parseia a resposta JSON
            console.log(xhr.responseText);
            var data = JSON.parse(xhr.responseText);
            
            // Obtém a lista de clientes do objeto JSON
            var offset1 = data.offset1;
            var offset2 = data.offset2;
            var offset3 = data.offset3;
            
            // Preenche o combobox com os clientes obtidos
            var offset1text = document.getElementById("offset1");
            var offset2text = document.getElementById("offset2");
            var offset3text = document.getElementById("offset3");

            console.log("offset1" + offset1);

            offset1text.value = parseFloat(offset1.replace(",", "."));
            offset2text.value = parseFloat(offset2.replace(",", "."));
            offset3text.value = parseFloat(offset3.replace(",", "."));
            
        }
    };
    xhr.send();
}

function loadClientes() {
    // Faz uma solicitação HTTP para obter os dados dos clientes
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `http://${SERVER_IP}/clientes`, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // Parseia a resposta JSON
            console.log(xhr.responseText);
            var data = JSON.parse(xhr.responseText);
            
            // Obtém a lista de clientes do objeto JSON
            var clientes = data.clientes;
            
            // Preenche o combobox com os clientes obtidos
            var select = document.getElementById("clientes");

            clientes.forEach(function(cliente) {
                var option = document.createElement("option");
                option.text = cliente;
                select.add(option);
            });
        }
    };
    xhr.send();
}



function loadTipos() {
    // Faz uma solicitação HTTP para obter os dados dos clientes
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `http://${SERVER_IP}/tipos`, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // Parseia a resposta JSON
            var data = JSON.parse(xhr.responseText);
            
            // Obtém a lista de clientes do objeto JSON
            var tipos = data.tipos;
            
            // Preenche o combobox com os clientes obtidos
            var select = document.getElementById("tipos");
            
            tipos.forEach(function(tipo) {
                var option = document.createElement("option");
                option.text = tipo;
                select.add(option);
            });
        }
    };
    xhr.send();
}

function loadTamanhos() {
    // Faz uma solicitação HTTP para obter os dados dos clientes
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `http://${SERVER_IP}/tamanhos`, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // Parseia a resposta JSON
            console.log(xhr.responseText)
            
            var data = JSON.parse(xhr.responseText);
            
            // Obtém a lista de clientes do objeto JSON
            var tamanhos = data.tamanhos;
            
            // Preenche o combobox com os clientes obtidos
            var select = document.getElementById("tamanhos");
            
            tamanhos.forEach(function(tamanho) {
                var option = document.createElement("option");
                option.text = tamanho[0];
                select.add(option);
                console.log(tamanho[0]);
                console.log(tamanho[1]);
                console.log(tamanho[2]);
            });
        }
    };
    xhr.send();
}

function filtrarClientes() {
    var input, filter, select, options, option, i, txtValue;
    input = document.getElementById("busca");
    filter = input.value.toUpperCase();
    select = document.getElementById("clientes");
    options = select.getElementsByTagName("option");
    for (i = 0; i < options.length; i++) {
        option = options[i];
        txtValue = option.textContent || option.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            option.style.display = "";
        } else {
            option.style.display = "none";
        }
    }
}

function toggleButton(buttonId) {
    var button = document.getElementById(buttonId);
    
    // Verifica se o botão está ligado (verde)
    if (button.classList.contains('ligado')) {
      // Desliga o botão (muda para vermelho)
      button.classList.remove('ligado');
      button.style.backgroundColor = 'red';
      //button.value = 'OFF';
    } else {
      // Liga o botão (muda para verde)
      button.classList.add('ligado');
      button.style.backgroundColor = 'green';
      //button.value = 'ON';
    }
}

function showPopup(message) {
    document.getElementById('popup').style.display = 'block';
    document.getElementById('popup-text').textContent = message
}

// Função para esconder o popup
function hidePopup() {
    document.getElementById('popup').style.display = 'none';
}

