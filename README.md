# AddUp.FakeRabbitMQ

[![Build](https://github.com/addupsolutions/AddUp.RabbitMQ.Fakes/workflows/Build/badge.svg)](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/actions?query=workflow%3ABuild)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=addupsolutions_AddUp.FakeRabbitMQ&metric=alert_status)](https://sonarcloud.io/dashboard?id=addupsolutions_AddUp.FakeRabbitMQ)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=addupsolutions_AddUp.FakeRabbitMQ&metric=coverage)](https://sonarcloud.io/dashboard?id=addupsolutions_AddUp.FakeRabbitMQ)
[![NuGet](https://img.shields.io/nuget/v/AddUp.FakeRabbitMQ.svg)](https://www.nuget.org/packages/AddUp.FakeRabbitMQ/)

## Important Notice

On _2020/10/04_, and starting with version 1.2.3, **AddUp.RabbitMQ.Fakes** was renamed to **AddUp.FakeRabbitMQ**. This concerns:

* This repository name: <https://github.com/addupsolutions/AddUp.FakeRabbitMQ>,
* The Nuget package name: <https://www.nuget.org/packages/AddUp.FakeRabbitMQ/>,
* The Assembly name: `AddUp.FakeRabbitMQ.dll`.

However, the root namespace remains (for compatibility purpose) `AddUp.RabbitMQ.Fakes`.

I deemed this renaming necessary as the `Fakes` suffix collides with [Microsoft Fakes](https://docs.microsoft.com/en-us/visualstudio/test/isolating-code-under-test-with-microsoft-fakes?view=vs-2019) product. Because the assembly name ends with `.Fakes`, several unit-test related tools consider it not to be a _real_ assembly but rather something generated by **Microsoft Fakes**.

## About

**AddUp.FakeRabbitMQ** is a fork of <https://github.com/Parametric/RabbitMQ.Fakes>. Thanks to the folks over there for their work without which our own version would probably never have been possible.

**AddUp.FakeRabbitMQ** provides fake implementations of the **RabbitMQ.Client** interfaces (see <https://www.nuget.org/packages/RabbitMQ.Client> for the nuget package and <https://github.com/rabbitmq/rabbitmq-dotnet-client> for its source code). They are intended to be used for testing so that unit tests that depend on RabbitMQ can be executed fully in memory withouth needing an external RabbitMQ server.

**AddUp.FakeRabbitMQ** builds on top of the original project:

* Targets **.NET Standard 2.0**
* Supports Default, Direct, Topic and Fanout exchange types.
  * _NB: Headers exchange type is not supported; however, it won't throw (it is implemented the same way as the Fanout type)_

## History

> Versions 1.x are based on [RabbitMQ .NET client Version 5.x](https://www.nuget.org/packages/RabbitMQ.Client/5.2.0)
>
> Versions 2.x are based on [RabbitMQ .NET client Version 6.x](https://www.nuget.org/packages/RabbitMQ.Client/6.2.4)

### [Version 2.1.0 - 2022/10/11](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v2.1.0)

* Updated **RabbitMQ.Client** to version [6.4.0](https://github.com/rabbitmq/rabbitmq-dotnet-client/blob/main/CHANGELOG.md#changes-between-631-and-640)

### [Version 2.0.0 - 2022/03/12](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v2.0.0)

* Based on **RabbitMQ.Client** version 6.2.4

### [Version 1.5.1 - 2022/03/12](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.5.1)

* Updated dependencies (obviously except for **RabbitMQ.Client** that is still version 5.2.0)

### [Version 1.5.0 - 2021/11/10](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.5.0)

> Mostly cleanup of the codebase as preparatory work for the v6 client implementation

* Code cleanup:
  * Removed useless overloads and members in `FakeModel`.
  * Refactored Unit Tests (split `IModel` tests into several classes, got rid of the _Arrange_, _Act_ and _Assert_ comments, renamed methods) + added a few ones.
* No-op implementation in `IModel.TxSelect`, `IModel.TxCommit` and `IModel.TxRollback` instead of throwing.
* Implemented `IModel.MessageCount(queue)` and `IModel.ConsumerCount(queue)` based on `QueueDeclarePassive`.
* `IModel.QueuePurge` now returns the number of purged messages.

### [Version 1.4.0 - 2021/11/06](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.4.0)

> This release merges the PR by @inbarbarkai. They are one year-old, and I'm ashamed it took me this long to merge them. Because I'm now _watching_ the repository, I should need less time to react in the future...
> Once again thanks for his contribution.

* BUGFIX: #30. `QueueDeclarePassive` now throws if the queue does not exist. Similarly, `ExchangeDeclarePassive` throws if the exchange does not exist.
* FEATURE: #29. Support for the [Alternate Exchange Feature](https://www.rabbitmq.com/ae.html).
* BUGFIX: #28. `DefaultBasicConsumer.IsRunning` property is now correctly returning true/false.

### [Version 1.3.2 - 2020/11/23](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.3.2)

* BUGFIX: #27. It was impossible to create a connection after having created and closed a previous connection.

### [Version 1.3.1 - 2020/11/23](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.3.1)

* BUGFIX: #25. Now it is possible to close a `Channel` after its connection was closed without the `Channel.Close` method throwing (this is consistent with how **RabbitMQ.Client** behaves).

### [Version 1.3.0 - 2020/11/09](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.3.0)

Most of the work in this release was contributed by @inbarbarkai. Thanks to him!

* As suggested by @inbarbarkai: Basic support for `IModel.ConfirmSelect` and the `IModel.WaitForConfirms*` family.
* Support for `IModel.CreateBasicPublishBatch` was added thanks to PR #21 by @inbarbarkai.
* Consumers that implement both `IBasicConsumer` **and** `IAsyncBasicConsumer` are correctly handled by `IModel.BasicConsume` and `IModel.BasicCancel` thanks to PR #23 by @inbarbarkai.

### [Version 1.2.3 - 2020/10/04](https://github.com/addupsolutions/AddUp.FakeRabbitMQ/releases/tag/v1.2.3)

* Renamed from **AddUp.RabbitMQ.Fakes** to **AddUp.FakeRabbitMQ**.

### [Version 1.2.2 - 2020/06/02](https://github.com/addupsolutions/AddUp.RabbitMQ.Fakes/releases/tag/v1.2.2)

* BUGFIX: Closing a connection could throw if owned models were closed before.

### [Version 1.2.1 - 2020/05/30](https://github.com/addupsolutions/AddUp.RabbitMQ.Fakes/releases/tag/v1.2.1)

* Updated to **RabbitMQ.Client** to version [5.2.0](https://github.com/rabbitmq/rabbitmq-dotnet-client/blob/master/CHANGELOG.md#changes-between-512-and-520)

### [Version 1.2.0 - 2019/12/11](https://github.com/addupsolutions/AddUp.RabbitMQ.Fakes/releases/tag/v1.2.0)

* First released version of **AddUp.RabbitMQ.Fakes**. Based on [RabbitMQ.Client version 5.1.2](https://www.nuget.org/packages/RabbitMQ.Client/5.1.2)
* _NB: the version starts at 1.2.0 so as not to collide with previous internal versions._

## License

This work is provided under the terms of the [MIT License](LICENSE).
