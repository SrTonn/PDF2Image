# PDF2Image

PDF2Image é uma aplicação de linha de comando desenvolvida em C# que converte páginas de arquivos PDF em imagens no formato PNG. Ele utiliza a biblioteca **PdfiumViewer** para renderizar e salvar cada página do PDF como uma imagem de alta qualidade.

## Funcionalidades

- Converte arquivos PDF em imagens PNG, salvando cada página como uma imagem separada.
- Suporte a entrada de arquivos e pastas:
    - **Arquivo PDF**: Converte um único PDF em imagens.
    - **Pasta com PDFs**: Converte todos os arquivos PDF de uma pasta em imagens.
- Gera uma pasta de saída para cada arquivo PDF, organizando as imagens por PDF.

## Uso

A aplicação pode ser executada via linha de comando com a seguinte sintaxe:

```bash
PDF2Image.exe <entrada> <saída>
```

## Parâmetros
- **entrada**: Caminho para o arquivo PDF ou para a pasta que contém arquivos PDF.
- **saída**: Caminho para a pasta onde as imagens serão salvas.

## Exemplo de Uso
1. Para converter um único arquivo PDF em imagens:

```bash
PDF2Image.exe "C:\caminho\para\arquivo.pdf" "C:\caminho\para\saída"
```

2. Para converter todos os arquivos PDF em uma pasta:
```bash
PDF2Image.exe "C:\caminho\para\pasta_de_pdfs" "C:\caminho\para\saída"
```

## Estrutura de Saída
A aplicação cria uma subpasta dentro do diretório de saída, nomeada com o prefixo [PDF] seguido do nome do arquivo PDF, onde as imagens são salvas com nomes sequenciais, ex: Page_1.png, Page_2.png, etc.

## Mensagens de Log
Durante a execução, a aplicação exibe mensagens no console, informando o progresso e resultados:

- Número de arquivos PDF encontrados na pasta de entrada.
- Criação de pastas de saída.
- Localização dos arquivos PNG gerados.

## Pré-requisitos
- .NET: Certifique-se de ter o .NET 8 ou superior instalado para executar a aplicação.
- Bibliotecas: A aplicação utiliza a biblioteca PdfiumViewer para a renderização de PDFs.

## Erros Comuns
1. **Nenhum PDF na pasta de entrada**: Quando o caminho de entrada é uma pasta sem arquivos PDF, a aplicação informará que não há PDFs disponíveis.
2. **Arquivo não encontrado**: Caso o caminho do PDF ou da pasta esteja incorreto, a aplicação não conseguirá acessar o arquivo.

## Contribuição
Sinta-se à vontade para contribuir, reportando problemas ou sugerindo melhorias.