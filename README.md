# ewave-livraria-plenoII
Projeto avaliação para a prova do cargo de Pleno II na empresa Ewave do Brasil, cargo de Programador no TJMT.


Foi desenvolvido com a linguagem C#, no Visual Studio 2019, utilizando os conceitos de DDD (Domain Driven Design), de modo que as diferentes partes e tipos de instrução da aplicação são colocados em diferentes projetos (do Visual Studio no caso) separando assim as camadas, facilitando a manutenção e melhoria contínua do projeto.

O front-end foi desenvolvido com Razor pages. Porém ainda se encontra no projeto da camada de aplicação, sendo necessária uma melhoria futura para que seja separado e movido para a camada de apresentação.

Os testes de unidade foram construídos com as ferramentas da própria Microsoft (biblioteca TestTools do Visual Studio). Também foi utilizado o framework MOCK que facilita a simulação de entidades de testes.


A aplicação está conteinerizada, ou seja, contida dentro do conteiner Docker cuja imagem (arquivo Docker Composer) se encontra na raíz da Solution.
