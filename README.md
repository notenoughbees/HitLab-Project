README


#### How to Play ####
==== Map Screen & Starting a Minigame ====
* Tap the "Scan a location" button once you're physically near the minigame you want to play [for the prototype, there's only one minigame, located at the Okeover Stream bridge next to the Zoology carpark & Forestry dept., so go there].
* Tap “Find Landmarks” to load the list of nearby landmarks.
* From the list, tap the minigame's associated location: the Okeover bridge minigame's location is called "Restoring Our Waterways II". The location will be at the top of the list if you're standing at that location.
* You're now on the VPS Localisation screen: move your device around the location until it localises. A hint image is shown in the bottom-left corner to remind you of which location you selected. If it's just a white square, that means it's still loading, so the localisation won't work: wait for that image to load before moving your device around.
* Once your device localises, the minigame starts! You should now see the minigame's title/intro screen.


==== Minigame: Okeover Bridge ====
* Click Play to dismiss the minigame's title/intro screen. The game sprites should be loaded: you should see the caddisfly eggs sitting on the rocks in the Okeover stream below. If they don't appear after several seconds, try restarting the app, and/or redoing the VPS localisation while standing in the centre of the minigame's VPS location, and don't move around until the sprites appear.
* Tap on the egg sac sprites to hatch the caddisflies!
* Caddisflies die when coming into contact with spiderwebs: swipe at the spiderwebs to get rid of them
* Caddisflies lay eggs periodically: keep tapping on the eggs to hatch more caddisflies
* You gain points for the more caddisfly eggs and flies you have: try to get as many points as you can before the 3-minute timer runs out!


#### Development Notes ####
* 
* For the minigame, the VPS mesh to use is called “Restoring Our Waterways II”. A second mesh called “John Britten Building” exists too, just for testing. Enable the one you want to use.
* 




#### TODO ####
This is a list of stuff I didn't have time to implement in the internship, but wanted to.
* Map markers
- Create markers on the map to show the minigame locations. The Niantic Emoji Garden sample app, and the Niantic Wayfarer app have map markers but I didn't know how to implement those. These apps' map markers were only for VPS wayspots, so the minigame locations would need to be VPS activated first, but these markers look good and have a photo of the wayspot too.
* Simplify the process to start a minigame
- Currently, you must press a button on the map screen, then press a button to load the list of landmarks, then select a landmark to go to the VPS Localisation screen for that landmark. This could probably be simplified so that you just press the button on the map screen and go to the VPS Localisation screen for the nearest VPS wayspot.
* "Regenerative Rating" score
- Discussed in the Google Doc: the RR is a sum of the high scores from all minigames.
*


#### Sample code used ####
* Map screen is based on xyz
* VPS Localisation screen is based on xyz
* Unity tutorials followed are linked in the code comments.
