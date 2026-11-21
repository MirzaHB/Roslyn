# Expected output

These come from a working solution run against the unmodified SampleApp. The
order of lines may differ from yours depending on how you iterate; the set of
lines should match.

## Exercise 1

```
Controllers/OrdersController.cs
  class OrdersController
    GetById(1)
    Create(2)
    Health(0)
Data/IOrderRepository.cs
  interface IOrderRepository
    GetById(1)
    Save(1)
Data/InMemoryOrderRepository.cs
  class InMemoryOrderRepository
    GetById(1)
    Save(1)
Framework/Attributes.cs
  class ApiControllerAttribute
  class RouteAttribute
  class HttpGetAttribute
  class HttpPostAttribute
  class ControllerBase
Models/Order.cs
  class Order
Services/IEmailSender.cs
  interface IEmailSender
    Send(2)
Services/IOrderService.cs
  interface IOrderService
    Find(1)
    Place(2)
Services/OrderService.cs
  class OrderService
    Find(1)
    Place(2)
    Validate(1)
Services/SmtpEmailSender.cs
  class SmtpEmailSender
    Send(2)
```

## Exercise 2

```
SampleApp.Controllers.OrdersController
  base: SampleApp.Framework.ControllerBase
SampleApp.Data.IOrderRepository
SampleApp.Data.InMemoryOrderRepository
  implements: SampleApp.Data.IOrderRepository
SampleApp.Framework.ApiControllerAttribute
  base: System.Attribute
SampleApp.Framework.RouteAttribute
  base: System.Attribute
SampleApp.Framework.HttpGetAttribute
  base: System.Attribute
SampleApp.Framework.HttpPostAttribute
  base: System.Attribute
SampleApp.Framework.ControllerBase
SampleApp.Models.Order
SampleApp.Services.IEmailSender
SampleApp.Services.IOrderService
SampleApp.Services.OrderService
  implements: SampleApp.Services.IOrderService
SampleApp.Services.SmtpEmailSender
  implements: SampleApp.Services.IEmailSender
```

`compilation.GetDiagnostics()` should contain no errors.

## Exercise 3

Part one, every call:

```
OrdersController.GetById -> IOrderService.Find
OrdersController.Create -> IOrderService.Place
InMemoryOrderRepository.GetById -> Enumerable.FirstOrDefault
InMemoryOrderRepository.Save -> List.Add
OrderService.Find -> IOrderRepository.GetById
OrderService.Place -> OrderService.Validate
OrderService.Place -> IOrderRepository.Save
OrderService.Place -> IEmailSender.Send
SmtpEmailSender.Send -> Console.WriteLine
```

Part two, only targets declared in SampleApp:

```
OrdersController.GetById -> IOrderService.Find
OrdersController.Create -> IOrderService.Place
OrderService.Find -> IOrderRepository.GetById
OrderService.Place -> OrderService.Validate
OrderService.Place -> IOrderRepository.Save
OrderService.Place -> IEmailSender.Send
```

## Exercise 4

```
GET /orders/{id} -> OrdersController.GetById
POST /orders -> OrdersController.Create
```

## Exercise 5

```
OrdersController depends on IOrderService
RouteAttribute depends on String
HttpGetAttribute depends on String
HttpPostAttribute depends on String
OrderService depends on IOrderRepository
OrderService depends on IEmailSender
```

The three attribute lines are correct: those constructors do take a string.
Filtering them out (for example, keeping only parameters whose type is an
interface or is declared in source) is a design decision you will face again
in the real project.

## Exercise 6

```
IOrderRepository is implemented by InMemoryOrderRepository
IEmailSender is implemented by SmtpEmailSender
IOrderService is implemented by OrderService

OrdersController.GetById -> OrderService.Find (via IOrderService.Find)
OrdersController.Create -> OrderService.Place (via IOrderService.Place)
OrderService.Find -> InMemoryOrderRepository.GetById (via IOrderRepository.GetById)
OrderService.Place -> OrderService.Validate
OrderService.Place -> InMemoryOrderRepository.Save (via IOrderRepository.Save)
OrderService.Place -> SmtpEmailSender.Send (via IEmailSender.Send)
```

## Exercise 7

```
Project: SampleApp
Documents: 9
Errors: 0
Types: 13
```

Feeding this compilation to your exercise 4 code should print the same two
endpoints as before.
