<!-- markdownlint-disable MD033 MD041 -->
<div align="center">

<img src="images/construction-icon.png" alt="HTTP BuildR" width="150px"/>

# HTTP BuildR

---

[![Nuget](https://img.shields.io/nuget/v/HttpBuildR.Request)](https://www.nuget.org/packages/HttpBuildR.Request/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Http-BuildR&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Http-BuildR)
[![CD Build](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/Http-BuildR/actions/workflows/check-markdown.yml)

Simple C# functions for building :hammer: requests and responses using only the
core System.Net.Http!

---

</div>
<!-- markdownlint-enable MD033 MD041 -->

## Why?

There are a ton of http client libraries out there, but nothing (that I liked)
that was just simple extensions to the core System.Net.Http classes.

I want the request and response building code to be,

* Declarative, it needs to flow and read as well as possible
* Simple, usage should be trivial and learning curve as flat as possible
* Complete, anything you can do with the core class can be done in a fluent
  declarative style
