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
* Npm and NodeJs (to fronend)

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

3 - Run the database create script in the follow link:
 https://github.com/wellyngtond2/Chat/tree/master/Scripts

## Final Considerations

* This project is focused on backend, so the front here is just minimal to see the project working.
* The fronend was made in ReactJs, so you need open the frontend folder in a terminal and run the command 'npm' to dowlonad the packages dependences, then run 'npm start' to run application.
* The frontend is waiting that backend run in localhost:5001, if was this different you must to update in all places on frontend.
* As the frontend is a minimal you must register new users by API swagger interface.
* To use the application by frontend insert you registered email and password:
![image](https://user-images.githubusercontent.com/13359384/205638061-d0525d0c-d96c-4e94-b0ff-d1ee7cf0a808.png)

* Then choose you room and send the messages:
![image](https://user-images.githubusercontent.com/13359384/205638109-4b1cf921-fbb1-4f21-ae94-583b633df00d.png)

* To create new rooms you must be it by API swagger.
