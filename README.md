minity
======

Open source Unity-based client for a video game called Minecraft.

Alternative subline: 'How to write a Minecraft client in a mere 365 days*!'

\* deadline not guaranteed

## Show me the screenies!

![screenshot 0](http://www.dropbox.com/s/i4uevf2v5qgqfl1/minity_ss_0.png)

## What's done?

Very little. Don't get your hopes too high this will ever pan out.

I've got a block renderer implemented in a shader that uses a 256x256x256 3D lookup texture, and 256+256+256 cuboids slicing the axes. It works! This renders a world made up of 256x256x256 blocks made up of ores and grass and stone and that kind of thing.

Because we're Unity-based, we can just use built-in Unity lighting and everything works :)

I can't do fences or doors or anything yet. I am not sure what to do about this at all.

Entities and creatures and players are not done, but they will use bog-standard Unity rendering.

No gameplay is done yet. Fair ways off yet.

No networking is done yet. You can't connect to anything.

## Minimalism

At the moment we're very minimalistic, in that I haven't written anything yet. However, the idea is to keep a very maintainable and hackable codebase, while supporting most of the features of the vanilla Minecraft client.

## Requirements

- Unity Pro (& you need to import the Pro Standard assets, they're not in git!)
- MineLib.Network & its dependencies are used, but some DLLs are already shipped so you don't need to worry unless you want to upgrade something.

## Performance

On my retina MacBook Pro with an Intel HD 5000 at 1024x640, we currently get 50 FPS staring from one edge of the world into the center - with the camera & fog set to only show 64 blocks in any direction. Not the best, but playable.

Update: 25 FPS with the full 256 block range! Woo!

On a desktop with modern AMD/Nvidia cards we can just render the whole 256^3 world at 1920x1080 and get >120 FPS.

This will be revised as the project zigzags its way to completion.

## Unity Free vs. Unity Pro

Targetting Unity Pro. Grab the 30 day trial. Sorry.

(It shouldn't be too challenging to get this going on Unity Free, this said. Contact me for versions of the shaders that don't use 3D textures.)

((Patches to make this work better with Unity Free without crapping up the codebase too much are welcomed readily.))

## License

Apache 2 licensed; do what thou wilt, more or less. Distribute it on your site or sell it if you want.

There are a couple of things I'd like you to do, though it's not a deal-breaker:

- Contribute fixes and code and the like back to the project, if it's appropriate! Let everyone benefit if it's not a competitive advantage you require.
- something else i can't remember right now

If you can't or don't want to do the above things, fine. I'd rather you used this code than didn't.

You **are** compelled to obey the Apache 2 license, though. Talk to me if that's a problem. We can make it work.
