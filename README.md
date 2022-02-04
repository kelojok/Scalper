# Scalper
## What's this?
I wanted to try out something called Chromedriver to scrape some content from pages related to ps5 and its stock status.

## Worker service?
The worker service executes a call to the API every minute and displays the result in a console window.

## Scraping?
The scraper class uses Chromedriver to spawn a window and inspect selected content, then takes different actions depending on if an element doesn't exist or if some text contains a certain word and so on.

## Setup instructions
Just add your address in the worker service (localhost) to contact the API and it should run smoothly.
Make sure both projects are running at the same time. Right-click solution > properties > start several projects.

I also have chrome version 98.0.4758.81 on my PC and chrome driver seems to be a bit sensitive to versions.
