using System;

public class Effect
{
	public int effId;

	public int typeEff;

	public int indexFrom;

	public int indexTo;

	public bool isNearPlayer;

	public const int NEAR_PLAYER = 0;

	public const int LOOP_NORMAL = 1;

	public const int LOOP_TRANS = 2;

	public const int BACKGROUND = 3;

	public const int CHAR = 4;

	public const int FIRE_TD = 0;

	public const int BIRD = 1;

	public const int FIRE_NAMEK = 2;

	public const int FIRE_SAYAI = 3;

	public const int FROG = 5;

	public const int CA = 4;

	public const int ECH = 6;

	public const int TACKE = 7;

	public const int RAN = 8;

	public const int KHI = 9;

	public const int GACON = 10;

	public const int DANONG = 11;

	public const int DANBUOM = 12;

	public const int QUA = 13;

	public const int THIENTHACH = 14;

	public const int CAVOI = 15;

	public const int NAM = 16;

	public const int RONGTHAN = 17;

	public const int BUOMBAY = 26;

	public const int KHUCGO = 27;

	public const int DOIBAY = 28;

	public const int CONMEO = 29;

	public const int LUATAT = 30;

	public const int ONGCONG = 31;

	public const int KHANGIA1 = 42;

	public const int KHANGIA2 = 43;

	public const int KHANGIA3 = 44;

	public const int KHANGIA4 = 45;

	public const int KHANGIA5 = 46;

	public Char c;

	public int t;

	public int currFrame;

	public int x;

	public int y;

	public int loop;

	public int tLoop;

	public int tLoopCount;

	private bool isPaint = true;

	public int layer;

	public int isStand;

	public static MyVector vEffData = new MyVector();

	public int trans;

	public static MyVector lastEff = new MyVector();

	public static MyVector newEff = new MyVector();

	private int[] khangia1 = new int[10] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };

	private int[] khangia2 = new int[10] { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };

	private int[] khangia3 = new int[10] { 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 };

	private int[] khangia4 = new int[10] { 6, 6, 6, 6, 6, 7, 7, 7, 7, 7 };

	private int[] khangia5 = new int[10] { 8, 8, 8, 8, 8, 9, 9, 9, 9, 9 };

	private bool isGetTime;

	public Effect()
	{
	}

	public Effect(int id, Char c, int layer, int loop, int loopCount, sbyte isStand)
	{
		this.c = c;
		effId = id;
		this.layer = layer;
		this.loop = loop;
		tLoop = loopCount;
		this.isStand = isStand;
		if (getEffDataById(id) == null)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = "/x" + mGraphics.zoomLevel + "/effectdata/" + id + "/data";
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			addEffData(effectData);
		}
		indexFrom = -1;
		indexTo = -1;
		trans = -1;
		typeEff = 4;
	}

	public Effect(int id, int x, int y, int layer, int loop, int loopCount)
	{
		this.x = x;
		this.y = y;
		effId = id;
		this.layer = layer;
		this.loop = loop;
		tLoop = loopCount;
		if (getEffDataById(id) == null)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = "/x" + mGraphics.zoomLevel + "/effectdata/" + id + "/data";
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			addEffData(effectData);
			lastEff.addElement(effId + string.Empty);
		}
		indexFrom = -1;
		indexTo = -1;
		typeEff = 1;
		if (!isExistNewEff(effId + string.Empty))
		{
			newEff.addElement(effId + string.Empty);
		}
	}

	public static void removeEffData(int id)
	{
		for (int i = 0; i < vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)vEffData.elementAt(i);
			if (effectData.ID == id)
			{
				vEffData.removeElement(effectData);
				break;
			}
		}
	}

	public static void addEffData(EffectData eff)
	{
		vEffData.addElement(eff);
		if (TileMap.mapID != 130 && vEffData.size() > 10)
		{
			for (int i = 0; i < 5; i++)
			{
				vEffData.removeElementAt(0);
			}
		}
	}

	public static EffectData getEffDataById(int id)
	{
		for (int i = 0; i < vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)vEffData.elementAt(i);
			if (effectData.ID == id)
			{
				return effectData;
			}
		}
		return null;
	}

	public static bool isExistNewEff(string id)
	{
		for (int i = 0; i < newEff.size(); i++)
		{
			string text = (string)newEff.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	public bool isPaintz()
	{
		if (!isPaint)
		{
			return false;
		}
		return true;
	}

	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		if (isPaintz() && getEffDataById(effId).img != null)
		{
			getEffDataById(effId).paintFrame(g, currFrame, x + xLayer, y + yLayer, trans, layer);
		}
	}

	public void getFrameKhangia()
	{
		if (effId == 42)
		{
			currFrame = khangia1[t];
		}
		if (effId == 43)
		{
			currFrame = khangia2[t];
		}
		if (effId == 44)
		{
			currFrame = khangia3[t];
		}
		if (effId == 45)
		{
			currFrame = khangia4[t];
		}
		if (effId == 46)
		{
			currFrame = khangia5[t];
		}
		t++;
		if (t > khangia1.Length - 1)
		{
			t = 0;
		}
	}

	public void paint(mGraphics g)
	{
		if (isPaintz() && getEffDataById(effId) != null && getEffDataById(effId).img != null)
		{
			getEffDataById(effId).paintFrame(g, currFrame, x, y, trans, layer);
		}
	}

	public void update()
	{
		try
		{
			if (effId >= 42 && effId <= 46)
			{
				getFrameKhangia();
			}
			else
			{
				if (getEffDataById(effId) == null || getEffDataById(effId).img == null)
				{
					return;
				}
				if (getEffDataById(effId).arrFrame != null)
				{
					if (!isGetTime)
					{
						isGetTime = true;
						int num = getEffDataById(effId).arrFrame.Length - 1;
						if (num > 0 && typeEff != 1)
						{
							t = Res.random(0, num);
						}
						if (typeEff == 0)
						{
							t = Res.random(indexFrom, indexTo);
						}
					}
					switch (typeEff)
					{
					case 4:
						x = c.cx;
						y = c.cy;
						if (t < getEffDataById(effId).arrFrame.Length)
						{
							t++;
						}
						break;
					case 1:
					case 3:
						if (t < getEffDataById(effId).arrFrame.Length)
						{
							t++;
						}
						break;
					case 0:
						if (Res.inRect(x - 50, y - 50, 100, 100, Char.myCharz().cx, Char.myCharz().cy) && t > indexFrom && t < indexTo)
						{
							if (t < indexTo)
							{
								t = indexTo;
							}
							isNearPlayer = true;
						}
						if (!isNearPlayer)
						{
							t++;
							if (t == indexTo)
							{
								t = indexFrom;
							}
						}
						else if (t < getEffDataById(effId).arrFrame.Length)
						{
							t++;
						}
						break;
					case 2:
						if (t < getEffDataById(effId).arrFrame.Length)
						{
							t++;
						}
						tLoopCount++;
						if (tLoopCount == tLoop)
						{
							tLoopCount = 0;
							trans = Res.random(0, 2);
						}
						break;
					}
					if (t <= getEffDataById(effId).arrFrame.Length - 1)
					{
						currFrame = getEffDataById(effId).arrFrame[t];
					}
				}
				if (t >= getEffDataById(effId).arrFrame.Length - 1)
				{
					if (typeEff == 0 || typeEff == 3)
					{
						isPaint = false;
					}
					if (tLoop == -1)
					{
						EffecMn.vEff.removeElement(this);
					}
					if (typeEff == 2)
					{
						t = 0;
						return;
					}
					if (typeEff == 4)
					{
						if (loop == -1)
						{
							t = 0;
							return;
						}
						tLoopCount++;
						if (tLoopCount == tLoop)
						{
							tLoopCount = 0;
							loop--;
							t = 0;
							if (loop == 0)
							{
								c.removeEffChar(0, effId);
							}
						}
						return;
					}
					isNearPlayer = false;
					if (loop == -1)
					{
						tLoopCount++;
						if (tLoopCount == tLoop)
						{
							tLoopCount = 0;
							t = 0;
							if (tLoop > 1)
							{
								trans = Res.random(0, 2);
							}
						}
						return;
					}
					tLoopCount++;
					if (tLoopCount == tLoop)
					{
						tLoopCount = 0;
						loop--;
						t = 0;
						if (loop == 0)
						{
							EffecMn.vEff.removeElement(this);
						}
					}
				}
				else
				{
					isPaint = true;
				}
			}
		}
		catch (Exception)
		{
			EffecMn.vEff.removeElement(this);
		}
	}
}
