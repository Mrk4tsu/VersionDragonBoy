public class EffectFeet : Effect2
{
	private int x;

	private int y;

	private int trans;

	private long endTime;

	private bool isF;

	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");

	public static void addFeet(int cx, int cy, int ctrans, int timeLengthInSecond, bool isCF)
	{
		EffectFeet effectFeet = new EffectFeet();
		effectFeet.x = cx;
		effectFeet.y = cy;
		effectFeet.trans = ctrans;
		effectFeet.isF = isCF;
		effectFeet.endTime = mSystem.currentTimeMillis() + timeLengthInSecond * 1000;
		Effect2.vEffectFeet.addElement(effectFeet);
	}

	public override void update()
	{
		if (mSystem.currentTimeMillis() - endTime > 0)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	public override void paint(mGraphics g)
	{
		int num = TileMap.size;
		if (TileMap.tileTypeAt(x + num / 2, y + 1, 4))
		{
			g.setClip(x / num * num, (y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt((x - num / 2) / num, (y + 1) / num) == 0)
		{
			g.setClip(x / num * num, (y - 30) / num * num, 100, 100);
		}
		else if (TileMap.tileTypeAt((x + num / 2) / num, (y + 1) / num) == 0)
		{
			g.setClip(x / num * num, (y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt(x - num / 2, y + 1, 8))
		{
			g.setClip(x / 24 * num, (y - 30) / num * num, num, 100);
		}
		g.drawRegion((!isF) ? imgFeet3 : imgFeet1, 0, 0, imgFeet1.getWidth(), imgFeet1.getHeight(), trans, x, y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}
}
