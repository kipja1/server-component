# Server component

This repository contains description, API documentation, usage examples and source code for Server component. Idea to create it originated from Ignitis challenge proposed in 2021. This component is a semester work for "Component based software engineering" module managed by Šarūnas Packevičius.

Project implemented by:
* Arnas Nakrošis
* Kipras Jasiūnas

## Main purpose of component and what problem it tries to solve?

This server component serves as a link between household or business user that consumes or generates electric energy. It is used to provide such data for electricity supplier or distributor in order to help with a very important problem – electric grid balancing. This is a big issue especially during peak hours which usually are in the morning and in the evening. Grid balancing became a significant issue in the last years due to increasing amount of electric vehicles being charged at home and especially if these are charged at peak hours. Renewable energy sources started to play a huge role in grid balancing. Although generating electricity from renewable energy is the main goal of global community, these sources can introduce oversupply. Scientific community became interested in such problem too – a scientific review performed in 2020 proves that supply and demand balancing is indeed a hot topic because number of published publications a year is increasing rapidly since 2015.

![Image of Publications](https://github.com/kipja1/server-component/blob/main/photos/publications.PNG)

## API documentation
API documentation was generated using Doxygen. It is available at https://kipja1.github.io/server-component/html/index.html

## User guide and usage example

In order for potential user (grid manager) to work with our created component, certain steps have to be made:

1.	User creates an application for data acquisition and/or visualizing
2.	Downloads and places Server component. 
3.	Includes published API to his application project
4.	Downloads User or Generator executable program
5.	Launches Server component and at least one of additional User or Generator applications
6.	Uses defined commands in his own application to gather data

## License
Our proposed solution is published under GNU General Public License v3.0.
