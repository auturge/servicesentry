<div id="top" align="right"><a href="https://github.com/auturge/servicesentry#top">(home)</a></div>

<h1 align="center">ServiceSentry Requirements</h1>

> [![Work In Progress][WIP-badge]](#top)
>
> This draft is a work in progress and is being provided for information purposes only.
>
> Because it is a work in progress, there are parts that are either missing or will be revised; the content will change. 
>
> Please, do not cite without the author’s permission.


## Table of Contents ##
- [Requirements](#requirements)
  - [Round 0](#round-0)


<a href="#top">(go to top)</a>

----

## Requirements ##

### Round 0 ###

* View and control (start/stop/etc.) Windows services that I care about in a GUI
* Notify me when a service falls over (send me an email)
* Squirrel away the logs that I care about
* Track the config files that I care about
* Put all of these things in one place
* Make it USABLE - the whole point is to make our jobs easier
* NO dependency on assemblies that are specific to &lt;insert corporation here&gt;
* (Windows) Service Oriented Architecture
  * Client (UI)
  * Monitor (Windows Service)
* Extensibility
* Notification Area Icon and menu (like WAMP, XAMPP)
  * Easy access to service toggling controls
  * notification of Monitor service status
* Quick access to a custom list of services
* (Session) logging - exposed in UI as well as log file(s)
* All configuration exposed in UI - no need to hand-roll config files
* Start with Windows
* Single-instance client application
* Extensions
  * Autoconfigure for various applications?
  * Add UI functionality (Services; ex: perform additional functionality on service stop/restart, like clearing some assembly cache)
* Internationalization (i18n; designed so that it can be adapted to various languages and regions without engineering changes)
* Localization (l10n; translating text and adding locale-specific components using the infrastructure provided by i18n) 

<a href="#top">(go to top)</a>

----

[WIP-badge]: https://img.shields.io/static/v1?label=WIP:&message=Work-in-Progress&color=blueviolet "This draft is a work in progress and is being provided for information purposes only.&#10;&#10;Because it is a work in progress, there are parts that are either missing or will be revised; the content will change. &#10;&#10;Please, do not cite without the author’s permission."