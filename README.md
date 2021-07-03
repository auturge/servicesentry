<h1 id="top" align="center">ServiceSentry</h1>

<p align="center">
  A Windows Service Monitoring and Management Suite.
</p>

<br>


[![auturge][auturge-badge]](#top)
[![License][license-image]][license-url]
[![Help Wanted][help-wanted-badge]][help-wanted-url]
[![Good First Issues][gfi-badge]][gfi-url]
[![Discord][discord-badge]][discord-url]

<br>

>
> [![Work In Progress][WIP-badge]](#top)
>
> ServiceSentry is a thing I built a while ago. 
> This is being added for posterity, as an example of my work.
> 
> It may not be fully functional, or even complete.
> I may clean it up and get it fully functional at some point.

<br>

## Introduction ##

Service Sentry is a Windows service monitoring and management suite. It exists as both 
1. a long-running background task which monitors a given set of services, and 
2. a thick client which lets you 
    - manage the configuration of the background task, and 
    - manage the states, configuration, and logs of the set of services being monitored

<br>

----

## Features ##

Service Sentry allows an administrator to 

- administer a specific list of services, individually or in groups, 
- designate critical services and receive warnings when critical services fail,
- view, open, and copy the service log files, or other designated log files,
- view, open, and copy the config files,
- archive and/or delete the service log files whenever the service is stopped or restarted,
- drag-and-drop log files from/to other applications (like Microsoft Outlook or Windows Explorer)

<br>

----
<!--

## Getting Started ##

@auturge/logger is available as source code from [GitHub][github-url], or as a minified package on [npm][npm-url].

> ```shell
> $ npm install @auturge/logger
> ```

<br>

----

## Support and Examples ##

- The tutorial is available [here](./docs/tutorial.md#top).
- API documentation can be found [here](./docs/api.md#top).
- Please post question and comments to the [discussions][discussions] page.

<br>

----
-->

## Contributing and Internal Documentation ##

The auturge family welcomes any contributor, small or big. We are happy to elaborate, guide you through the source code and find issues you might want to work on! To get started have a look at our [documentation on contributing][contributing].

<br>

----

## License ##

Distributed under the MIT license. See [`LICENSE`][license] for more information.

<br>

[WIP-badge]: https://img.shields.io/static/v1?label=WIP:&message=Work-in-Progress&color=blueviolet
[home]: https://github.com/auturge/servicesentry#top
[discussions]: https://github.com/auturge/servicesentry/discussions/


[github-url]: https://github.com/auturge/servicesentry
[help-wanted-badge]: https://img.shields.io/github/issues/auturge/logger/help%20wanted?color=%232EA043&label=help%20wanted&style=flat-square
[help-wanted-url]: https://github.com/auturge/servicesentry/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22

[gfi-badge]: https://img.shields.io/github/issues/auturge/servicesentry/good%20first%20issue?color=%23512BD4&label=good%20first%20issue&style=flat-square
[gfi-url]: https://github.com/auturge/servicesentry/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22

[discord-badge]: https://img.shields.io/discord/860951617301774348?style=flat-square&label=Discord&logo=discord&logoColor=white&color=7289DA
[discord-url]: https://discord.com/api/guilds/860951617301774348/widget.json

[auturge-badge]: https://img.shields.io/badge/Auturge-blueviolet.svg
[auturge-github-homepage]: https://github.com/auturge/auturge#top

[contributing]: https://github.com/auturge/auturge/blob/master/docs/CONTRIBUTING.md#top

[license]: https://github.com/auturge/auturge/blob/master/LICENSE
[license-image]: http://img.shields.io/:license-mit-blue.svg?style=flat-square
[license-url]: http://badges.mit-license.org
