# Cloudy Tic-Tac-Toe

A game of tic-tac-toe that will be a real-world app which uses cloud-based services.

Here is a list of things I am using in no particular order, and with some details as to why I picked them:

* **Git** - https://git-scm.com  
  Because Final_vs_finalfinal_last_release_published_v3_forreal.zip is not the way we do things when people are watching us.
* **GitHub** - https://github.com  
  Because this is what I use before and where all my projects are. I have not actually tried the others like 
  GitLab/Bitbucket so I cannot really compare.
  * **GitHub Actions** - https://github.com/features/actions  
    It is easy to get started and I found I can do templating using local actions. For example, I have a folder
    [`setup-tools`](https://github.com/mattleibow/CloudyTicTacToe/tree/2917564505f54ed88d792735e6087eee35035b43/.github/workflows/setup-tools)
    which is an "action" that 
    [I can then reference](https://github.com/mattleibow/CloudyTicTacToe/blob/2917564505f54ed88d792735e6087eee35035b43/.github/workflows/build.yml#L18-L19)
    in my [main yaml](https://github.com/mattleibow/CloudyTicTacToe/blob/2917564505f54ed88d792735e6087eee35035b43/.github/workflows/build.yml).
  * **GitHub Projects** - https://github.com/features/issues  
* **Rider for macOS** - https://jetbrains.com/rider  
  I used to use Visual Studio for Mac but that is mostly deprecated and not recieving new support for .NET 8. Visual
  Studio Code is great, but it lacks a few essential features (multi-targeting support).
* **Visual Studio Code** - https://code.visualstudio.com  
  Because it is the best code editor in the world :) I have instaled the C# Dev Kit and .NET MAUI Dev Kit. I use it to
  edit this file and do a bunch of file based operations. In other projects that I am working on, I can use it just
  fine. For example, the [DeviceRunners](https://github.com/mattleibow/DeviceRunners) project that I will be
  integrating soon, I use VSCode to build and run tests.
* **xUnit** - https://xunit.net/  
  Unit tests are essential and any real app needs to have some automated tests. I picked xUnit since that was the most
  recent framework I have been using. I used to use NUnit and MSTest, but in most cases the framework used is not as 
  important as the tests being written. Let the team decide what is most productive. For me, xUnit was the one we are
  actively using so it makes sense.
* **.NET MAUI**  
  Becasue...
  * **XAML**  
  Because...
  * **.NET MAUI Community Toolkit**  
  Reasons...
* **MORE**  
  BECAUSE

## 1. Idea

I had an Idea for 2024. I want to make a "real world" app for the various app stores - but do it in such a way that I
can learn about new things. I work on .NET MAUI so I know some XAML and C# ;) but my cloud and backend skills are not
so amazing.

This is a new series where I am hoping to make a simple app - 1 or 2 player tic-tac-toe game - and build it with a
backend in mind. I have no idea what I will use or what I will do, but I have some goals:

1. Focus on the backend
2. Track analytics and crashes
3. Support authenticated users for those score-conscious people
4. Support game results for leaderboards or  some "advanced player metrics"
5. Use XAML for the mobile/desktop app
6. Use Blazor WASM for the web version
7. Learn
8. Have fun
9. Make sure there are tests: unit tests, device tests and UI tests
10. Maybe some ads for that sweet cash (jk, to see how a real app developer would want to do this)
11. Some remote config or A/B testing for the advanced/new features
12. Push notifications
13. Cloud, stateless functions
14. Beta testers
15. Website for the landing pages and various other "docs"
16. Maybe some AI/ML for player suggestions
17. Track all the dev like a real app in GitHub: https://github.com/mattleibow/CloudyTicTacToe

There is a lot in the list above, but it really is a small list for things to make a fun and engaging app, but be able
to learn from players and to make decisions for future development.

Follow along with the progress on GitHub: https://github.com/mattleibow/CloudyTicTacToe

## 2. First lines

To get started quickly, I am planning on making a simple random-place computer player so I can start focusing on the
cloud aspects.

Project board: https://github.com/users/mattleibow/projects/2/views/1

I am planning on a simple navigation:

1. Landing page with a Play button that goes to Game screen
2. Game screen
3. Game results pop-over with a New Game button to screen 2 or Exit to screen 1

To actually get going, I first pushed the File | New template app up to GitHub to make all the future changes less
scary.

## 3. CI

Once I got some code going I needed to make sure I did not break anything later on. I used GitHub Actions to set up a
basic pipeline that just builds the code and makes sure the compiler is happy. 

I am leveraging a small local action to handle the setup process - both because I want to reuse the step, but also
clean up the main pipeline by hiding some details. The local action has a bunch of install steps that have no real
bearing on the app, like "install .NET" or "install .NET tools". The main pipeline file has the actual work, which
is simple and is just a few steps: "install", "build" and then "upload artifacts".

## 4. Basic game code

I wanted to write some basic UI code to "feel" some of the game navigation. I created 3 pages and set up the
navigation. One thing I observed is that I was not happy with my original idea of having ther results page actually
be a separate page. This was a slightly jarring effect when the game is over we totally lose the context.

To fix this, I tried using a popup page where it results mearly overlayed the current game. This felt more normal so
I decided to stick with that. My initial popup was going to be a Shell modal page with popover effects, but this was
not quite what I wanted. In the end, I opted for the .NET MAUI Community Toolkit `Popup` control and I am pretty
happy with it.

This left me with a three pages, and a "simulated gameplay navigation".

During the process of testing things out, I found there was a bug on macOS where action sheets did not appear. I was
trying not to get distracted, but in the end I did and then went ahead and fixed it...

## 5. Distractions

So I got distracted working on an annoying thing I found. On macOS, it appears there is a small difference with the
way action sheets work and so the .NET MAUI code actualy crashes/does nothing: 

 - Bug: https://github.com/dotnet/maui/issues/18156
 - My PR: https://github.com/dotnet/maui/pull/19629

The fix was easy so I shoulght I would do it. However, the UI tests took long to write because we had no way of
getting the alerts and determing what was presented. Anyways, I finished that so back to tic-tac-toe!

## 6. Better gameplay

Once I had all the navigation in place and it "felt" OK, I started on the actual game logic.

My approach was to start creating types in code and adding details. I wanted to be able to save and restore games - and
maybe even have an undo feature... This did mean I went a bit overboard for a tic-tac-toe game, but the whole point
wasn't the game but the entire process and services used.

I ended up with a few types in my first pass:

* `GameBoard` - This was effectively a wrapper for a 2D array of positions. It also had a few extra methods to
  make moves and to calculate if a player won or lost.
* `GameState` - This was a small type to basically hold the current state of a game. It was more of a "is the game
  over?" and "who won and where?".
* `Player` - This was a typed wrapper for an `int` which was a "player number".
* `WinningPosition` - A struct to hold what type of line was made (horizontal, vertical, diagonal) and where the start
  of the line was so we can do fun things later.
* `WinningType` - The enum that holds the type of line in the `WinningPosition` struct.

I created a few tests to make sure my new types were constructable and did a few things as I expected. I also added
some tests to see if my winning checks were correct.

## 7. ViewModels and pages

Once I had a "game system" implemented, I wanted to make things click and play. I set up a few view models so I could
use databinding in my pages to make things happen. I had three view models:

* `GameViewModel` - This was the main view model and handled the user doing something in the UI and making sure
  the game logic responded. It would hadle a move, switch to the next player and ensure all the game state was
  re-calculated. When a game end was determined, it would rais an event for the page.
* `BoardViewModel` - This was the wrapper for the `GameBoard` and just handled the operations to place a piece on the
  board. The various UI buttons would also bind to this so they can update the UI aspect of the board.
* `ResultsViewModel` - This was the simple view model to handle the data when a game was over and make sure it was
  manipulated for UI presentation.

The pages were also updated with a few extra interactive elements and were bound to the view models:

* `GameBoardPage` is the main game page and was bound to the `GameViewModel`. The individual buttons for the available
  places were bound to the `BoardViewModel`.
* `GameResultsPage` is the popup that was bound to the `ResultsViewModel`.
* `HomePage` does not yet have a view model as it does nothing right now.
