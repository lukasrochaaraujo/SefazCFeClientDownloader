# Sefaz CFe Client Downloader
Client to circumvent restrictions on CFe download from the [Secretaria da Fazenda (Sefaz)](https://servicos.sefaz.ce.gov.b) portal

Step 1 - Go to [secure environment](https://servicos.sefaz.ce.gov.br/internet/acessoseguro/servicosenha/logarusuario/login.asp) and log in

Step 2 - Take TAXID an TOKEN from some header request

Step 3 - Make sure that you have the [MFe](https://servicos.sefaz.ce.gov.br/internet/download/projetomfe/perguntasrespostas.pdf) serial number

Step 4 - Instance the ```CFeService``` class and start to download files
