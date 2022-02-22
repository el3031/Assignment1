Elaine Lee
el3031
2/22/2022
MacBook Air (Early 2014)
iPhone 11 iOS14.6
Device name: iPhone
Video link: https://drive.google.com/file/d/1OdnmlWGmsC1vJ4N4upEZ-NK-9DK_ylT4/view?usp=sharing

This game features a duck walking through a forest. At
the end of its journey, it goes to a party with some
friends.

The biggest issue I had was with rotations, especially
in scene 4 (platform 4). It was very difficult to get
the player to rotate correctly in relation to the
ground, and still allow rotation via finger dragging.
In the end, after lots of reading through the Unity
API and other internet resources, I found the
Quaternion.LookRotation() function that seamlessly
allowed me to rotate the player according to the
ground while preserving the forward direction.

In addition, the player movements in scene 1 and 2
were also a lot of trouble. In the beginning,
the duck would sometimes move in the complete
opposite direction or keep moving even when the user
was tapping on the spot it was at. I solved these
issues by calculating movement vectors in relation
to the duck's transform.position, and by setting
the vector to zero when the target was close to 
the actual spot.

Technical issues also abounded as my computer is 
extremely old, sometimes causing Unity to crash.

Assets:
- Party hats from DANKIE
- Duck and other animals from JKT_Art
- Rocks, flowers, mushrooms, etc. from Pure Poly
