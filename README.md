# LightDetector
A light detection example for Coty

## Use
Nothing happens in game view, everything happens in scene view. Switch over and drag around the light to see the detection. The detected areas are marked with blue. You can duplicate the lights to add another one, but the detection won't happen for the old light until you drag it again. This is because the probes are currently working under trigger enter and leave. 

In the ideal situation, the lights are never moving. When a light is destroyed or created, you will run through each probe and check for light. This won't use on trigger enter and leave, it will check the trigger outright.

## Notes
- If you use a BoxCollider for the light trigger, the size of the collider is double for each coordinate
	- LightRadius = 10 | BoxCollider.size = new Vector3(20, 20, 20)
