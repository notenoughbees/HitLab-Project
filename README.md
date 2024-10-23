# HitLab Regenerative Tourism Prototype App
This is a prototype app for the HitLab regenerative tourism research project, created by Danielle Sim for a PACE495 internship during 2024 Semester 2. It implements a map screen and one minigame, and is built off of the Niantic Lightship Map Resource Game [sample project](https://lightship.dev/docs/maps/sample_projects/#map-based-resource-game). Further notes on the project, such as my project log and brainstorming of the game concepts and gameplay are in [this](https://docs.google.com/document/d/1tDF5xV8388KVuVDwxj86CrIm0jnS1esgIW8BEym90tw/edit?usp=sharing) Google Doc (this link has viewing permissions only).

## Concept
The minigame in this prototype is inspired by the information board located at the bridge where it takes place. The info board states how the Okeover river is home to the caddisflies, but spiderwebs at the end of the river are dangerous to the flies as they get caught in them. The minigame then, is to save the caddisflies by swiping at the spiderwebs. The game starts with a few caddisfly egg sacs placed on the rocks in the stream, which the player taps to make hatch into caddisflies. The flies then lay eggs semi-periodically, but spiderwebs appear in random locations semi-periodically too. The player earns points for each egg laid and for each fly hatched, but loses points when the flies fly into the webs. The player must hatch as many flies as possible while destroying all the webs for 3 minutes, to get the best score. 

This minigame encourages regenerative tourism because it gets players to care about keeping this population of flies alive. Also, it avoids making players want to kill spiders because the enemy in the game is just their webs, not the spiders themselves.

## Requirements
This project was made using Unity XYZ and Lightship ARDK XYZ.

## How to Play
(Since it's still a prototype, these instructions are aimed at players who are also developers)
#### Map Screen; Starting a Minigame
* Tap the "Scan a Location" button once you're physically near the minigame you want to play. In the prototype, there's only one minigame, which is located at the Okeover Stream bridge next to the Zoology carpark and Forestry department, so go there to play the minigame.
* Tap “Find Landmarks” to load the list of nearby landmarks.
* From the landmarks list, tap the VPS wayspot that corresponds to the minigame: the Okeover bridge minigame's wayspot is called "[Restoring Our Waterways II](https://lightship.dev/account/geospatial-browser/-43.5224852277752,172.58739325297188,16.56,9304F0D191A345278282AEB0BE7F7B6C,295631c30f804d4eacc36314a8133e37.16/scan-wayspot)". The wayspot will be at the top of the list if you're standing at that location.
* Now, on the VPS Localisation screen, move your device around the location until it localises. A hint image is shown in the bottom-left corner to remind you of which location you selected. If it's just a white square, that means it's still loading, so the localisation won't work: wait for that image to load before moving your device around. If you're having trouble localising, look up the VPS wayspot on the [Geospatial Browser](https://lightship.dev/account/geospatial-browser) (requires login) or look at the mesh in Unity to see what parts of the location you're meant to be aiming at (note that the hint image isn't necessarily of the localisable area!).
* Once your device localises, the minigame starts! You'll now be on the minigame's title/intro screen.

#### Minigame: Okeover Bridge Caddisfly Game
* Click Play to dismiss the minigame title/intro screen and start the timer. The game sprites should be loaded: you should see the caddisfly eggs sitting on the rocks in the Okeover stream below. If they don't appear after several seconds, try restarting the app, and/or redoing the VPS localisation while standing in the centre of the wayspot, and don't move around until the sprites appear. In my experience, the sprites tend to load the fastest and in the most accurate location when you're standing at the origin of the VPS wayspot ([this](https://lightship.dev/docs/ardk/sample_projects/#vps-localization) sample shows where that is).
* Tap on the egg sac sprites to hatch the caddisflies!
* Caddisflies die when coming into contact with spiderwebs: swipe at the spiderwebs to get rid of them.
* Caddisflies lay eggs periodically: keep tapping on the eggs when they appear to hatch more caddisflies.
* You gain points for the more caddisfly eggs and flies you have: try to get as many points as you can before the 3-minute timer runs out!

## Development Notes
* For the minigame, the VPS mesh to use is called “Restoring Our Waterways II”. A second mesh called “John Britten Building” exists in the project too, just for testing. Enable the one you want to use.
* The sprites are all 2D just because finding free 2D pictures was easier than finding free 3D assets. They;re intended to be 3D though, so they have 3D colliders. The spiders and flies also have a billboarding effect.

## Future Ideas
This is a list of stuff I didn't have time to implement in the internship, but wanted to.
##### Map markers
Create markers on the map to show the minigame locations. The Niantic [Emoji Garden](https://lightship.dev/docs/ardk/emoji_garden/) sample app, and the [Niantic Wayfarer](https://play.google.com/store/apps/details?id=com.nianticlabs.argeo.niamapapp&pcampaignid=web_share) app have map markers but I didn't know how to implement those. These apps' map markers are only for VPS wayspots, so the minigame locations would need to be VPS activated (which is intended anyway since you scan the wayspot to start the game), but these markers look good and have a photo of the wayspot too.
##### Simplify the process to start a minigame
Currently, you must press a button on the map screen, then press a button to load the list of landmarks, then select a landmark to go to the VPS Localisation screen for that landmark. This could probably be simplified so that you just press the button on the map screen and go to the VPS Localisation screen for the nearest VPS wayspot.
##### "Regenerative Rating" score
Discussed in the Google Doc: the RR is a sum of the high scores from all minigames. This combined score encourages doing all of the minigames and trying to be more regenerative when playing them, to get a better RR.

## Sample code used
* The map screen is based on the [Map-based Resource Game](https://lightship.dev/docs/maps/sample_projects/#map-based-resource-game).
* The VPS Localisation screen is based on the [VPS Localisation](https://lightship.dev/docs/ardk/sample_projects/#vps-localization) sample.
* Playback sample is from [Alive](https://youtu.be/rJHBvh28dV4?si=4idiOsBgUnbMhld8) [Studios](https://drive.google.com/drive/folders/1vS9mHet8JYzekdEmvQa1riYwANRPwAL3).
* Unity tutorials followed are linked in the code comments.