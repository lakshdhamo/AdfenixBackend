# Backend application

## Problem
1.	It is a synchronous program
2.	It looks structured programming
3.	Values are hard coded in the class
4.	Current structure will be complicated when system grows and hard to understand
5.	No logging
6.	Exception is not handled properly

## Solution:
1.	Made everything as **Asynchronous** call as much as possible
	* Used async await
2.	Used **Parallel.ForEach** to trigger simultaneous call
	* Since ServerIds are independent to each other, Fetch/Send the server data simultaneously. Improved the performance by using parallelism.
3.	Used **logging** mechanism
	* Used logging to capture Info/Warn/Error for better debugging purpose
4.	Used **Retry mechanism**
	* Used retry mechanism to avoid external server uncertainty. External server might fail due to several reason, some might be deadlock, busy, resource not available, etc,. Retry logic is customizable. So, we can decide how many times to retry, how often, any specific exception to retry, etc.
5.	Handled proper **exception handling**
	* Added TryCatch in all the possible places. And captured the log
6.	Used latest **HttpClient** module
	* Replaces WebClient with HttpClient, because WebClient is obsolete. 
7.	Used **config file**
	* Moved the hard coded values to config file - appsettings.json
8.	Used **Dependency Injection**
	* Used DI to inject the dependent services such as IMediator, IDataReadService, IDataWriteService, ILogService.
	* DI ensures the loosely coupled and resolved the dependencies.
9.	Used **Dto** for Input/Output values
	* Not exposing the Entities outside. Used Dto for Input/Output data handling.
10.	Performed **Model validation**
11.	Used Extended **Strategy pattern**
	* Extending the Strategy Pattern for parameterized Server logic
	* Current system is fetching count from two different servers. It might grow in future. Need to have a generic algorithm that should perform differently based on the input. Here, we have two different server logics but differ with Parameter. So, extended the Strategy pattern with customized parameter list.
	* The strategy pattern decouples servers from the class that uses them allowing the server logics to vary independently. It does not, however, allow server logics to have different parameters. The solution provided here addresses the case when the logics have different sets of parameters, and when the user is allowed to see and modify these parameters for each concrete server logic before its execution. This is accomplished by introducing special parameter classes which encapsulate class parameters and have certain responsibilities (e.g. boundary values checking). The abstract base class is completely decoupled from parameters letting each concrete class create its own list of parameter instances which mirrors its parameters
12.	Implemented **CQRS pattern**
	* CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. CQRS in your application can maximize its performance, scalability, and security. It has advantages of Single Responsibility Principle, Independent Scaling, Separation of Concern.
13. Implemented **Mediator pattern** using MediatR for separation of concern. 
	* This pattern provides a mediator class which normally handles all the communications between different classes and supports easy maintenance of the code by loose coupling.
	
**Note:** I proposed this solution by keeping big picture in mind. This solution can be simplified based on the requirement.
