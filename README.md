
# 🌾 AgroSolutions - History API (Microsserviço de Histórico)

A **AgroSolutions History API** é o microsserviço backend (Core) responsável pela ingestão, armazenamento e disponibilização do histórico de telemetria dos sensores agrícolas da plataforma AgroSolutions. 

Desenvolvido com foco em alta performance e escalabilidade, este serviço processa dados provenientes de diferentes tipos de sensores (Solo, Meteorologia e Silos) e fornece os dados para os painéis de monitorização (Dashboards).

## 🚀 Tecnologias Utilizadas

* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Base de Dados (NoSQL):** MongoDB (Persistência de dados de telemetria)
* **Motor de Busca / Logs:** Elasticsearch 7.17
* **Visualização de Logs:** Kibana
* **Autenticação:** JWT (JSON Web Tokens) Bearer
* **Observabilidade:** Serilog (Integração direta com o Elasticsearch e consola)
* **Containerização:** Docker e Docker Compose
* **Orquestração:** Kubernetes (Amazon EKS)
* **CI/CD:** GitHub Actions

## 🏗️ Arquitetura (Clean Architecture)

O projeto está estruturado em camadas para garantir a separação de responsabilidades e facilitar a manutenção e os testes:

* `AgroSolutions.History.API`: Ponto de entrada da aplicação, onde estão definidos os Controllers REST, configuração do Swagger, Middlewares e Injeção de Dependências.
* `AgroSolutions.History.Application`: Contém a lógica de negócio, os serviços (`ISensorService`), mapeamentos de dados e os DTOs (Data Transfer Objects).
* `AgroSolutions.History.Domain`: O coração do sistema. Contém as entidades de domínio, objetos de valor específicos por sensor (ex: `SoilData`, `WeatherData`, `SiloData`), excepções de domínio e interfaces de repositórios.
* `AgroSolutions.History.Infrastructure`: Implementação da persistência de dados. Configuração do `MongoDbContext`, repositórios e mapeamento das classes no MongoDB.

## ✨ Funcionalidades

* **Ingestão de Dados:** Receção de dados estruturados com base no tipo de sensor:
  * **Solo:** Humidade, pH e Nutrientes NPK (Nitrogénio, Fósforo, Potássio).
  * **Clima (Meteorológica):** Temperatura, Humidade, Velocidade/Direção do Vento, Precipitação e Ponto de Orvalho.
  * **Silo:** Nível de Preenchimento (%), Temperatura Média e Níveis de CO2 (ppm).
* **Consulta de Histórico:** Endpoints otimizados para fornecimento de séries temporais com filtros por `SensorId`, `FieldId`, `Type` e intervalo de datas (`StartDate`, `EndDate`).
* **Autenticação Centralizada:** Validação de tokens JWT assinados pelo Keycloak do ecossistema.

## ⚙️ Como Executar Localmente

### Ambiente Completo via Docker Compose
O projeto dispõe de um ficheiro `docker-compose.yml` que sobe instantaneamente toda a infraestrutura necessária (API, Elasticsearch e Kibana). 

*Atenção: A API procura uma instância local do MongoDB. Certifique-se de ter um contentor do Mongo a correr na porta 27017, ou ajuste a ConnectionString no compose.*

    docker-compose up -d
Após o arranque:

-   **API / Swagger:** `http://localhost:5000/swagger`
-   **Kibana (Observabilidade):** `http://localhost:5601`
-   **Elasticsearch:** `http://localhost:9200`

### Apenas a Aplicação (.NET CLI)

Se desejar correr apenas o código .NET para debug:

    cd AgroSolutions.History.API 
    dotnet run
## 🚀 CI/CD e Deploy (Kubernetes na AWS)

O ciclo de vida da aplicação é automatizado através de workflows do **GitHub Actions**.

**Regra de Deploy:** A esteira de publicação é acionada unicamente quando um **Pull Request** com origem da branch `development` é integrado (merged) na branch `main`.

**Fluxo Automatizado:**

1.  A ação faz o checkout do código.
    
2.  Faz o build da imagem Docker (usando o `Dockerfile` com o SDK .NET 10 e imagem final ASP.NET 10 leve).
    
3.  Realiza o Push da imagem para o **Amazon ECR** (Elastic Container Registry).
    
4.  Substitui as variáveis de ambiente dinâmicas nos manifestos da pasta `k8s/` (`deployment.yaml` e `service.yaml`).
    
5.  Aplica os ficheiros no cluster **Amazon EKS**.
    

O serviço corre na AWS dentro do Namespace definido e fica acessível internamente para o API Gateway (Ocelot) fazer o roteamento inteligente de pedidos.
