# Jogo da Velha Online e Local

Este repositório contém um projeto desenvolvido utilizando Nuxt.js para o frontend, .NET Core para o backend e Redis para armazenamento de dados temporários. O projeto consiste em um jogo da velha que pode ser jogado tanto online quanto local.

## Funcionalidades

- **Jogo da Velha Online**: Permite que os usuários joguem o jogo da velha com outros jogadores online. O gerenciamento de jogos online é feito usando SignalR.
- **Jogo da Velha Local**: Fornece a opção de jogar contra a inteligência artificial do computador, que utiliza o algoritmo Minimax.
- **Armazenamento em Redis**: Utiliza Redis para armazenar temporariamente os dados do jogo durante as partidas online.
- **Docker Compose**: Facilita a configuração e execução do projeto em contêineres Docker.

## Pré-requisitos

Certifique-se de ter os seguintes pré-requisitos instalados em sua máquina:

- Docker
- Docker Compose

## Como Executar

1. Clone este repositório:

    ```
    git clone https://github.com/rcoliveira2016/game-da-velha.git
    ```

2. Navegue até o diretório do projeto:

    ```
    cd game-da-velha
    ```

3. Execute o Docker Compose:

    ```
    docker-compose up --build
    ```

4. Uma vez que os contêineres estejam em execução, acesse o aplicativo em seu navegador web:

    - **Endereço**: [http://localhost:3000](http://localhost:3000)
   
## Estrutura do Projeto

- **`/frontend`**: Contém o código fonte do frontend desenvolvido com Nuxt.js.
- **`/backend`**: Contém o código fonte do backend desenvolvido com .NET Core.
- **`docker-compose.yml`**: Arquivo de configuração do Docker Compose para executar os contêineres necessários.
- **`README.md`**: Este arquivo, contendo informações sobre o projeto e instruções de uso.

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues para reportar problemas ou propor novas funcionalidades. Pull requests também são aceitos.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
