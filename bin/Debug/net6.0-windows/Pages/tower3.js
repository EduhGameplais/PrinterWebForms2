const FIRST_PRINTER = 7;
const TOWER_ID = 3;

const SERVER_IP = window.location.hostname;

function resetPrinting(printer) {
    makeRequest(`http://${SERVER_IP}/print/restart?tower=${TOWER_ID}&printer=${printer}`);
}
function cancelPrinting(printer) {
    makeRequest(`http://${SERVER_IP}/print/cancel?tower=${TOWER_ID}&printer=${printer}`);
}

function sendPrint() {
    "use strict"
    var button  = document.getElementById("imp01");
    var button2 = document.getElementById("imp02");
    var button3 = document.getElementById("imp03");

    var copies = document.getElementById("numchap").value;
    if(copies == 0){
        showPopup('Por favor digite a quantidade de chapas.');
        return;
    }

    let NumPerPrinter1 = 0;
    let NumPerPrinter2 = 0;
    let NumPerPrinter3 = 0;
    let Next = 1;

    for (let count = 0; count < copies;) {
        if (Next === 1) {
            Next = 2;
            if (button.classList.contains('ligado')) {
                count += 5;
                NumPerPrinter1++;
            }
        } else if (Next === 2) {
            Next = 3;
            if (button2.classList.contains('ligado')) {
                count += 5;
                NumPerPrinter2++;
            }
        } else if (Next === 3) {
            Next = 1;
            if (button3.classList.contains('ligado')) {
                count += 5;
                NumPerPrinter3++;
            }
        }
    }

    if (((NumPerPrinter2 + NumPerPrinter1 + NumPerPrinter3) * 5) - copies !== 0) {
        if (button.classList.contains('ligado')) {
            NumPerPrinter1++;
        } else if (button2.classList.contains('ligado')) {
            NumPerPrinter2++;
        } else if (button3.classList.contains('ligado')) {
            NumPerPrinter3++;
        }
    }

    var cliente = document.getElementById("clientes").value;
    if(cliente == "Selecione um cliente") {
        showPopup('Por favor selecione um cliente.');
        return;
    }

    var boxsizeName = document.getElementById("tamanhos").value;
    if(boxsizeName == "Selecione um"){
        showPopup('Por favor selecione um tamanho da caixa.');
        return;
    }

    var tipo = document.getElementById("tipos").value;
    if(tipo == "Tipo de Caixa"){
        showPopup('Por favor selecione um tipo de caixa.');
        return;
    }

    var offset1 = document.getElementById("offset1").value;
    var offset2 = document.getElementById("offset2").value;
    var offset3 = document.getElementById("offset3").value;

    if (offset1 == ""){
        offset1 = 0;
    }
    if (offset2 == ""){
        offset2 = 0;
    }
    if (offset3 == ""){
        offset3 = 0;
    }

    console.log(NumPerPrinter1,NumPerPrinter2,NumPerPrinter3);

    if(NumPerPrinter1 > 0) 
        makeRequest(`http://${SERVER_IP}/edit?cliente=${cliente}&boxsizeName=${boxsizeName}&offset=${offset1}&imagesPerPrint=5&tipo=${tipo}&copies=${NumPerPrinter1}&tower=${TOWER_ID}&printer=1`);
        
    if(NumPerPrinter2 > 0)
        makeRequest(`http://${SERVER_IP}/edit?cliente=${cliente}&boxsizeName=${boxsizeName}&offset=${offset2}&imagesPerPrint=5&tipo=${tipo}&copies=${NumPerPrinter2}&tower=${TOWER_ID}&printer=2`);

    if(NumPerPrinter3 > 0)
        makeRequest(`http://${SERVER_IP}/edit?cliente=${cliente}&boxsizeName=${boxsizeName}&offset=${offset3}&imagesPerPrint=5&tipo=${tipo}&copies=${NumPerPrinter3}&tower=${TOWER_ID}&printer=3`);
    
    
    // Verifica se o botão está ligado (verde)
    if (button.classList.contains('ligado')) {
        if(NumPerPrinter1 > 0) 
        makeRequest(`http://${SERVER_IP}/print?tower=${TOWER_ID}&printer=1`);
        showPopup("Aguardando confirmação do servidor...");
    }
    if (button2.classList.contains('ligado')) {
        if(NumPerPrinter2 > 0) 
        makeRequest(`http://${SERVER_IP}/print?tower=${TOWER_ID}&printer=2`);
        showPopup("Aguardando confirmação do servidor...");
    }
    if (button3.classList.contains('ligado')) {
        if(NumPerPrinter3 > 0) 
        makeRequest(`http://${SERVER_IP}/print?tower=${TOWER_ID}&printer=3`);
        showPopup("Aguardando resposta do servidor...");
    }
}


function saveOffsets() { 

    var offset1 = document.getElementById("offset1").value.replace(".", ",");
    var offset2 = document.getElementById("offset2").value.replace(".", ",");
    var offset3 = document.getElementById("offset3").value.replace(".", ",");

    makeRequest(`http://${SERVER_IP}/edit?offset=${offset1}&tower=${TOWER_ID}&printer=1`);
    makeRequest(`http://${SERVER_IP}/edit?offset=${offset2}&tower=${TOWER_ID}&printer=2`);
    makeRequest(`http://${SERVER_IP}/edit?offset=${offset3}&tower=${TOWER_ID}&printer=3`);
}

function makeRequest(url) {
    console.log(url);
    var xhr = new XMLHttpRequest();
    showPopup("Aguardando resposta do servidor...");
    xhr.open("GET", url, true);
    xhr.onreadystatechange = function () {
        try {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    // Parseia a resposta JSON
                    var responseText = xhr.responseText;

                    if (responseText === `success`) {
                        showPopup(`Impressão reiniciada com sucesso`);
                        return;
                    } else if (responseText === `error`) {
                        showPopup(`Ocorreu um erro ao reiniciar a impressão.\nMais detalhes no console do notebook.`);
                        return;
                    } else if (responseText === `not_jobs_avaliable1`) {
                        showPopup(`Impressora ${FIRST_PRINTER} não tem trabalhos de impressão para reiniciar.`);
                        return;
                    } else if (responseText === `not_jobs_avaliable2`) {
                        showPopup(`Impressora ${FIRST_PRINTER+1} não tem trabalhos de impressão para reiniciar.`);
                        return;
                    } else if (responseText === `not_jobs_avaliable3`) {
                        showPopup(`Impressora ${FIRST_PRINTER+2} não tem trabalhos de impressão para reiniciar.`);
                        return;
                    } else if (responseText === `png-not-exists`) {
                        showPopup(`Não foi possível encontrar o PNG desse cliente com essas configurações.`);
                        return;
                    } else if (responseText === `printing-started1`) {
                        showPopup(`Impressão inciada(IMP${FIRST_PRINTER}).`);
                        return;
                    } else if (responseText === `printing-started2`) {
                        showPopup(`Impressão inciada(IMP${FIRST_PRINTER+1}).`);
                        return;
                    } else if (responseText === `printing-started3`) {
                        showPopup(`Impressão inciada(IMP${FIRST_PRINTER+2}).`);
                        return;    
                    } else if (responseText === `edit-offset-success`) {
                        showPopup("Offsets foram salvos com sucesso.");
                        return;
                    } else {
                        // Tentar parsear como JSON
                        var data = JSON.parse(responseText);
                        var result = data.result;

                        if (result === `Settings to access printer 'IMP${FIRST_PRINTER}' are not valid.`) {
                            showPopup(`ERRO: Ocorreu um erro ao tentar imprimir na Impressora ${FIRST_PRINTER}.`);
                            return;
                        } else if (result === `Settings to access printer 'IMP${FIRST_PRINTER+1}' are not valid.`) {
                            showPopup(`ERRO: Ocorreu um erro ao tentar imprimir na Impressora ${FIRST_PRINTER+1}.`);
                            return;
                        } else if (result === `Settings to access printer 'IMP${FIRST_PRINTER+2}' are not valid.`) {
                            showPopup(`ERRO: Ocorreu um erro ao tentar imprimir na Impressora ${FIRST_PRINTER+2}.`);
                            return;
                        } else {
                            console.log("Resposta não esperada:", xhr.responseText);
                        }
                        
                    }
                    showPopup("Sucesso.");
                } else {
                    // Aqui capturamos erros de HTTP (como 404, 500, etc.)
                    console.error(`Erro na requisição: ${xhr.status}`);
                    showPopup(`Erro na comunicação com o servidor. Tente novamente em 5 segundos.(${xhr.status})`);
                }
            }
        } catch (error) {
            console.error("Erro na requisição:", error);
            showPopup("Erro na comunicação com o servidor. Tente novamente em 5 segundos.");
        }
    };

    xhr.send();
}
