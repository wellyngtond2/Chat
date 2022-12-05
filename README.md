# Chat
### Chat is a backend focused application where I implemented many software concepts and tecnologies, such as: 
* CQRS
* Mediator
* SignalR
* Clean Arquitecture
* RabbitMQ
* Domain Events
* Ef Core
* Quartz
* AutoMapper
* Minimal Api's

## The solution contains the follow structure :
![image](https://user-images.githubusercontent.com/13359384/205627209-514b8945-973f-4e3e-ba73-8a7cd351e12f.png)

## How to Start

### Prerequisites

#### You will need the follow tecnologies available

* .Net 6
* RabbitMq 
* SQL Server Instance

#### Steps

1 - You must to start "Chat.Api" and "GetStockBot" in the same time, to do it go to the solution and right click then in Properties select the option "Multi startup projects" and select the Start option for them:

![image](https://user-images.githubusercontent.com/13359384/205627974-3850e4e6-a26b-4356-beef-2a043195b4f2.png)

2 - Setup the appsettings.json in both applications such as the follow images:
2.A Chat.Api appsettings.Json:

![image](https://user-images.githubusercontent.com/13359384/205629499-1c99fa37-29ee-4d37-b6c9-67b5f0cf8949.png)

- "ConnectionStrings: DefaultConnection" is a sql server connection string.
- "QueueSettings: ConnectionString" is a RabbitMq connection string.
- "BackgroundJobSettings: ApiScheduleIntervalInSeconds": is how long time the application background service should waiting to schedule in seconds.
- 
2.B GetStockBot appsettings.Json:

![image](https://user-images.githubusercontent.com/13359384/205630285-e9e59531-d3af-4415-b1d2-e57f6da263da.png)


- "StockSettings: BaseUrl" is a base url where we will to get the stock info.
- "QueueSettings: ConnectionString" is a RabbitMq connection string.
- "BackgroundJobSettings: BotScheduleIntervalInSeconds": is how long time the application background service should waiting to schedule in seconds.

