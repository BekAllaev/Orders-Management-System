# Orders Manaegment System

Order Management System WPF application which works with orders, based on database Northwind. 

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


