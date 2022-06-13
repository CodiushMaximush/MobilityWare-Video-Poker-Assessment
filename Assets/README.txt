Hello, and welcome to Cody Salmond's take on Video Poker. 
Feel free to poke around, everything should be commented. 

You can import custom deck images by creating an instance of Standard Deck Data. 

1. Right click the assets panel
2. Choose Create-> Card Games -> Deck
3. Fill out the fields
4. Right Click the title of the inspector panel for the Deck
5. Select 'Generate Deck Data'
6. Check for errors and fix any typos in your path or symbol names until it works. 

Swap the deck by replacing the referenced 'Standard Deck Data' under 'Asset Dependencies' on the Dealer Object/Monobehavior. 


There was one small bug with this version of Unity where you can't inspect Animator States after you add an exit transition to them:
https://issuetracker.unity3d.com/issues/inspector-not-displaying-state-and-transition-properties-once-duplicated
and the solution provided there didn't work for me so I was a bit limited in what I could do as far as animations. Including fixing a
small bug where 'winning' cards don't sync up their animations if some of them were detected in the first hand. 

I suppose one could argue that this is a feature though.

Excited to hear back from you. 

	- Cody Salmond