<div id="top" align="right"><a href="https://github.com/auturge/servicesentry#top">(home)</a></div>

<h1 align="center">ServiceSentry Design Overview</h1>

<div style="text-align:center"><img src="https://img.shields.io/static/v1?label=WIP:&message=Work-in-Progress&color=blueviolet" title="This draft is a work in progress and is being provided for information purposes only.&#10;&#10;Because it is a work in progress, there are parts that are either missing or will be revised; the content will change. &#10;&#10;Please, do not cite without the author’s permission."
/></div>

## Table of Contents ##
- [Purpose of ServiceSentry](#purpose-of-servicesentry)
- [Milestones](#milestones)
  - [Milestone 1][mile1]
	- [Agent](#agent)
    - [Client](#client)
  - [Milestone 2][mile2]
	- [Warden](#warden)
    - [Oversight](#oversight)
- [Project Tracking](#project-tracking)

<a href="#top">(go to top)</a>

----

## Purpose of ServiceSentry ##

* View and control (start/stop/etc.) Windows services that I care about in a GUI
* Notify me when a service falls over (send me an email)
* Squirrel away the logs that I care about
* Track the config files that I care about
* Put all of these things in one place
* Make it USABLE - the whole point is to make our jobs easier

<a href="#top">(go to top)</a>

----

## Milestones ##

ServiceSentry has several development milestones:

### Milestone 1: Service ("Agent") and Client ("Client") ###

#### Agent ####

A long-running Windows service that monitors service status, and handles restarting, emailing, etc.

#### Client ####

A "thick" client (C# WPF) used to configure and interact with the local Agent service

> Milestone 1 will be complete and operational before significant effort is made to implement anything in Milestone 2, however, requirements for Milestone 2 *will* be considered when designing/implementing Milestone 1. 

### Milestone 2: Ecosystem Control ("Warden") and Web Client ("Oversight") ###

#### Warden ####

A long-running Windows service that monitors the status and behavior of multiple Agent services, as well as collecting performance and failure metrics.

#### Oversight ####

A cross-platform (html/css/js) web page used to configure and interact with the Warden service.

For more details on specific requirements, see the requirements pages.

<a href="#top">(go to top)</a>

----

[WIP-badge]: https://img.shields.io/static/v1?label=WIP:&message=Work-in-Progress&color=blueviolet

[mile1]: #Milestone-1-Service-Agent-and-Client-Client
[mile2]: #Milestone-2-Ecosystem-Control-Warden-and-web-client-oversight