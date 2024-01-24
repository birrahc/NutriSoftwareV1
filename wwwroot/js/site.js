function AbrirModal(modalId) {
    $("#" + modalId).modal('show');
    Mascaras();
}

function FecharModal(modalId) {
    $("#" + modalId).modal('hide');
}

function AbrirFormularioEditar(url, id, pnlResutado, modalId) {
    console.log("Id " + id)
    $.ajax({
        type: 'GET',
        url: url,
        data: { Id: id },
        success: function (response) {
            $('#' + pnlResutado).html(response);
            AbrirModal(modalId);
            Mascaras();
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Erro...',
                text: 'Não foi possivel abrir o formumario',
                footer: '<a href>Why do I have this issue?</a>'
            });
        }
    });
}

function CadastrarEditar(url, formName, pnlResutado, modalId, mensagemSucesso) {
    let formularioEnvio = $('form[name="' + formName + '"]').serialize();

    console.log(formularioEnvio)

    $.ajax({
        type: 'POST',
        url: url,
        data: formularioEnvio,
        success: function (response) {
            Swal.fire({
                position: 'top',
                icon: 'success',
                title: mensagemSucesso,
                showConfirmButton: true,
                timer: 47000
            });

            FecharModal(modalId);
            $('#' + pnlResutado).html(response);
        },
        error: function (response) {
            var obj = jQuery.parseJSON(response.responseText);
            console.log(obj);
            //toastr.error(obj.MensagemDeErro.replaceAll("\r\n\r\n", "<br/>").replaceAll("\n", "<br/>"), obj.TituloDoErro);
            Swal.fire({
                icon: 'error',
                title: obj.TituloErro,
                text: obj.MensagemDeErro.replaceAll('\n','\n\n'),
                footer: ''
            });
        }
    });
}

function Deletar(url, id, pnlResultado, mensagemSucesso,mensagemAlet) {

    Swal.fire({
        title: mensagemAlet,
        text: "Não será possivel reverter a operação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim'
    }).then((result) => {
        if (result.value) {

            $.ajax({
                type: 'POST',
                url: url,
                data: { Id: id },
                success: function (response) {
                    Swal.fire({
                        position: 'top',
                        icon: 'success',
                        title: mensagemSucesso,
                        showConfirmButton: true,
                        timer: 47000
                    });
                    $('#' + pnlResultado).html(response);
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro...',
                        text: 'Não foi possivel abrir o formumario',
                        footer: '<a href>Why do I have this issue?</a>'
                    });
                }
            });
        }
    })

   
}

function TextNumericDecimal(inputId, maximoDecimal) {

    let valor = $("#" + inputId).val();

    if (valor != null && valor != "") {
        // Remove caracteres não numéricos, exceto vírgulas e pontos
        valor = valor.replace(/[^\d,-]/g, '');

        // Remove múltiplas vírgulas ou pontos e mantém apenas o último
        valor = valor.replace(/([,])[,]+/g, '$1');

        // Garante que há apenas uma vírgula no valor
        let partes = valor.split(',');
        if (partes.length > 2) {
            partes = [partes[0], partes.slice(1).join('')];
            valor = partes.join(',');
        }

        // Limita a maximoDecimal números após a vírgula
        partes = valor.split(',');
        if (partes.length === 2 && partes[1].length > maximoDecimal) {
            partes[1] = partes[1].slice(0, maximoDecimal);
            valor = partes.join(',');
        }

        // Atualiza o valor do campo
        $("#" + inputId).val(valor);
    }
}

function Detalhes(url, pnlResultado) {

    $.ajax({
        type: 'GET',
        url: url,
        data: {requestJs:true},
        success: function (response) {
            history.pushState(null, null, url);
            $('#' + pnlResultado).html(response);
            Mascaras();
           
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Erro...',
                text: 'Não foi possivel buscar os dados',
                footer: ''
            });
        }
    });
}

function convertFormToJSON(form) {
    return $(form)
        .serializeArray()
        .reduce(function (json, { name, value }) {
            json[name] = value;
            return json;
        }, {});
}

function ModificaUrl(urlModify, formDestino) {
    if ((urlModify != null && urlModify != "") && (formDestino != null && formDestino != "")) {
        let form = $('form[name=' + formDestino + ']');
        let dados = convertFormToJSON(form);
        let parametros = "/" + dados.CodigoProduto
        urlModify = urlModify.lastIndexOf(parametros) >= 0 ? url : (urlModify + parametros);
        history.pushState(null, null, urlModify);
    }
}

function Mascaras()
{
    $('input[mask="cpf"]').mask('000.000.000-00', { reverse: true });
    $('span[mask="cpf"]').mask('000.000.000-00', { reverse: true });
}

function ConverterFormularioParaJson(formName) {
    var formData = $('form[name="' + formName + '"]').serializeArray();

    // Converter o objeto para um objeto JavaScript
    var formObject = {};
    $.each(formData, function (index, field) {
        formObject[field.name] = field.value;
    });

    // Converter o objeto JavaScript para JSON
    var jsonData = JSON.stringify(formObject);
}