# Orders Manaegment System

If you are a business client then Order Management System is application which works with orders, based on database Northwind. It has dashboard where you can see statistics and graphics which shows you information of your buisness at the moment. Also you can create orders with great UX/UI views.

But if you are a developer then you will be interested in how this project solve one treacky problem. This application works with huge amount of data and must stay UI-friendly and responsive at the same time. This problem solved using reactive programming and exactly ReactiveUI framework.

Another interesting aspect is that WPF client can receive data from WebAPI.

In this application you can see  
Programming language: `C#`  
Frameworks: `Prism` , `ReactiveUI` , `DynamicData`  
Patterns: MVVM, Dependency Injection  
Containers: `IOC container Unity`  
Data Storage: `Microsoft SQL Server 2017 `  
UI Markup: `XAML`  
UI Controls: `Syncfusion WPF Contorls 2019`  
Reporting: `Mirosoft RDLC + Syncfusion Report Viewer`  
Styling: `Material Design Themes`  
Animations: `WPF animations` 

### IMPORTANT #1
You need to have Northwind database on your `Microsoft SQL Server`    

### IMPORTANT #2
Don`t forget to turn on backend in order if you will access remote database 
  
## Prism + ReactiveUI + DynamicData
Responsibilities of Prism: Navigation + Modularity  
Responsibilities of ReactiveUI: ViewModels + Messaging + Other useful functions(Like event to command)
Responsibilities of DynamicData: Read, write, and edit of collections

### Prism
Prism gave an opportunity to build composite application. App was divide into several modules
where each view of module was divided into regions. Also PRISM make possible to 
handle navigation events.

### ReactiveUI
This framework provided base class for all viewmodels and opportunity to send messages between viewmodels.

### DynamicData 
Fast UI would not be possible, if there were not `Dynamic Data` , which fills, edits, filters and do a lot of other operations
above collections in asynchronous style.

### Change between Local and Remote repository
This application consume data from Local and Remote repositories. If you want to change repository you can find   
`OMSWPFClien.exe.config` file in bin->Debug and change app setting `AccessRepository`'s value to Remote or Local.


## Screenshoots

Dashboard 

![](https://github.com/Allaev1/Orders-Management-System/blob/master/OMS.WPFClient/Screenshoots/Dashboard.jpg?raw=true)

Creation view

![](https://github.com/Allaev1/Orders-Management-System/blob/master/OMS.WPFClient/Screenshoots/Create%20View.jpg?raw=true)

Here you will see notification pane in action. Database server was stopped and application caught exception and shows the error to the user. Otherwise application would collapsed.  

![](https://github.com/Allaev1/Orders-Management-System/blob/master/OMS.WPFClient/Screenshoots/Notification.jpg?raw=true)
