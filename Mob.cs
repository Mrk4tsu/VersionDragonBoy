using System;
using Assets.src.g;

public class Mob : IMapObject
{
	public const sbyte TYPE_DUNG = 0;

	public const sbyte TYPE_DI = 1;

	public const sbyte TYPE_NHAY = 2;

	public const sbyte TYPE_LET = 3;

	public const sbyte TYPE_BAY = 4;

	public const sbyte TYPE_BAY_DAU = 5;

	public static MobTemplate[] arrMobTemplate;

	public const sbyte MA_INHELL = 0;

	public const sbyte MA_DEADFLY = 1;

	public const sbyte MA_STANDWAIT = 2;

	public const sbyte MA_ATTACK = 3;

	public const sbyte MA_STANDFLY = 4;

	public const sbyte MA_WALK = 5;

	public const sbyte MA_FALL = 6;

	public const sbyte MA_INJURE = 7;

	public bool changBody;

	public short smallBody;

	public bool isHintFocus;

	public string flystring;

	public int flyx;

	public int flyy;

	public int flyIndex;

	public bool isFreez;

	public int seconds;

	public long last;

	public long cur;

	public int holdEffID;

	public int hp;

	public int maxHp;

	public int x;

	public int y;

	public int dir = 1;

	public int dirV = 1;

	public int status;

	public int p1;

	public int p2;

	public int p3;

	public int xFirst;

	public int yFirst;

	public int vy;

	public int exp;

	public int w;

	public int h;

	public int hpInjure;

	public int charIndex;

	public int timeStatus;

	public int mobId;

	public bool isx;

	public bool isy;

	public bool isDisable;

	public bool isDontMove;

	public bool isFire;

	public bool isIce;

	public bool isWind;

	public bool isDie;

	public MyVector vMobMove = new MyVector();

	public bool isGo;

	public string mobName;

	public int templateId;

	public short pointx;

	public short pointy;

	public Char cFocus;

	public int dame;

	public int dameMp;

	public int sys;

	public sbyte levelBoss;

	public sbyte level;

	public bool isBoss;

	public bool isMobMe;

	public static MyVector lastMob = new MyVector();

	public static MyVector newMob = new MyVector();

	public int xSd;

	public int ySd;

	private bool isOutMap;

	private int wCount;

	public bool isShadown = true;

	private int tick;

	private int frame;

	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	private bool wy;

	private int wt;

	private int fy;

	private int ty;

	public int typeSuperEff;

	public bool isBusyAttackSomeOne = true;

	public int[] stand = new int[12]
	{
		0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
		1, 1
	};

	public int[] move = new int[15]
	{
		1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
		3, 3, 2, 2, 2
	};

	public int[] moveFast = new int[7] { 1, 1, 2, 2, 3, 3, 2 };

	public int[] attack1 = new int[3] { 4, 5, 6 };

	public int[] attack2 = new int[3] { 7, 8, 9 };

	public int[] hurt = new int[1];

	private sbyte[] cou = new sbyte[2] { -1, 1 };

	public Char injureBy;

	public bool injureThenDie;

	public Mob mobToAttack;

	public int forceWait;

	public bool blindEff;

	public bool sleepEff;

	private int[][] frameArr = new int[6][]
	{
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 }
	};

	private bool isGetFr = true;

	public Mob()
	{
	}

	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, int hp, sbyte level, int maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		xFirst = (x = (this.pointx = pointx));
		yFirst = (y = (this.pointy = pointy));
		this.status = status;
		if (templateId != 70)
		{
			checkData();
			getData();
		}
		if (!isExistNewMob(templateId + string.Empty))
		{
			newMob.addElement(templateId + string.Empty);
		}
		maxHp = maxp;
		this.levelBoss = levelBoss;
		isDie = false;
		xSd = pointx;
		ySd = pointy;
		if (isNewModStand())
		{
			stand = new int[17]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			move = new int[17]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			moveFast = new int[17]
			{
				0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
				2, 2, 2, 2, 2, 2, 2
			};
			attack1 = new int[12]
			{
				3, 3, 3, 3, 4, 4, 4, 4, 5, 5,
				5, 5
			};
			attack2 = new int[12]
			{
				3, 3, 3, 3, 4, 4, 4, 4, 5, 5,
				5, 5
			};
		}
		else if (isNewMod())
		{
			stand = new int[12]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
				1, 1
			};
			move = new int[16]
			{
				1, 1, 1, 1, 2, 2, 2, 2, 1, 1,
				1, 1, 3, 3, 3, 3
			};
			moveFast = new int[8] { 1, 1, 2, 2, 1, 1, 3, 3 };
			attack1 = new int[11]
			{
				4, 4, 4, 5, 5, 5, 6, 6, 6, 6,
				6
			};
			attack2 = new int[11]
			{
				7, 7, 7, 8, 8, 8, 9, 9, 9, 9,
				9
			};
		}
		else if (isSpecial())
		{
			stand = new int[12]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
				1, 1
			};
			move = new int[16]
			{
				2, 2, 3, 3, 2, 2, 4, 4, 2, 2,
				3, 3, 2, 2, 4, 4
			};
			moveFast = new int[8] { 2, 2, 3, 3, 2, 2, 4, 4 };
			attack1 = new int[8] { 5, 6, 7, 8, 9, 10, 11, 12 };
			attack2 = new int[4] { 5, 12, 13, 14 };
		}
		else
		{
			stand = new int[12]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
				1, 1
			};
			move = new int[15]
			{
				1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
				3, 3, 2, 2, 2
			};
			moveFast = new int[7] { 1, 1, 2, 2, 3, 3, 2 };
			attack1 = new int[3] { 4, 5, 6 };
			attack2 = new int[3] { 7, 8, 9 };
		}
	}

	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	public void getData()
	{
		if (arrMobTemplate[templateId].data == null)
		{
			arrMobTemplate[templateId].data = new EffectData();
			string text = "/Mob/" + templateId;
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				arrMobTemplate[templateId].data.readData(text + "/data");
				arrMobTemplate[templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(templateId);
			}
			lastMob.addElement(templateId + string.Empty);
		}
		else
		{
			w = arrMobTemplate[templateId].data.width;
			h = arrMobTemplate[templateId].data.height;
		}
	}

	public void setBody(short id)
	{
		changBody = true;
		smallBody = id;
	}

	public void clearBody()
	{
		changBody = false;
	}

	public static bool isExistNewMob(string id)
	{
		for (int i = 0; i < newMob.size(); i++)
		{
			string text = (string)newMob.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < arrMobTemplate.Length; i++)
		{
			if (arrMobTemplate[i].data != null)
			{
				num++;
			}
		}
		if (num < 10)
		{
			return;
		}
		for (int j = 0; j < arrMobTemplate.Length; j++)
		{
			if (arrMobTemplate[j].data != null && num > 5)
			{
				arrMobTemplate[j].data = null;
			}
		}
	}

	public void checkFrameTick(int[] array)
	{
		if (tick > array.Length - 1)
		{
			tick = 0;
		}
		frame = array[tick];
		tick++;
	}

	private void updateShadown()
	{
		int num = TileMap.size;
		xSd = x;
		wCount = 0;
		if (ySd <= 0 || TileMap.tileTypeAt(xSd, ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(xSd / num, ySd / num) == 0)
		{
			isOutMap = true;
		}
		else if (TileMap.tileTypeAt(xSd / num, ySd / num) != 0 && !TileMap.tileTypeAt(xSd, ySd, 2))
		{
			xSd = x;
			ySd = y;
			isOutMap = false;
		}
		while (isOutMap && wCount < 10)
		{
			wCount++;
			ySd += 24;
			if (TileMap.tileTypeAt(xSd, ySd, 2))
			{
				if (ySd % 24 != 0)
				{
					ySd -= ySd % 24;
				}
				break;
			}
		}
	}

	private void paintShadow(mGraphics g)
	{
		int num = TileMap.size;
		if (TileMap.tileTypeAt(xSd + num / 2, ySd + 1, 4))
		{
			g.setClip(xSd / num * num, (ySd - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt((xSd - num / 2) / num, (ySd + 1) / num) == 0)
		{
			g.setClip(xSd / num * num, (ySd - 30) / num * num, 100, 100);
		}
		else if (TileMap.tileTypeAt((xSd + num / 2) / num, (ySd + 1) / num) == 0)
		{
			g.setClip(xSd / num * num, (ySd - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt(xSd - num / 2, ySd + 1, 8))
		{
			g.setClip(xSd / 24 * num, (ySd - 30) / num * num, num, 100);
		}
		g.drawImage(TileMap.bong, xSd, ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	public void updateSuperEff()
	{
		if (typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		if (typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	public virtual void update()
	{
		GetFrame();
		if (blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, x, y, 1);
		}
		if (sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, x, y, 3, 1, 1));
		}
		if (!GameCanvas.lowGraphic && status != 1 && status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + mobId * 2) == 0)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
				{
					Char char2 = new Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					if (@char.cgender == 0)
					{
						MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), -100, -100, char2, 25);
					}
				}
			}
			if (Char.myCharz().isFlyAndCharge && Char.myCharz().cf == 32)
			{
				Char char3 = new Char();
				char3.cx = Char.myCharz().cx;
				char3.cy = Char.myCharz().cy - Char.myCharz().ch;
				if (Char.myCharz().cgender == 0)
				{
					MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), -100, -100, char3, 25);
				}
			}
		}
		if (holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
		{
			EffecMn.addEff(new Effect(holdEffID, x, y + 24, 3, 5, 1));
		}
		if (isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, x, y, 1);
			}
			long num = mSystem.currentTimeMillis();
			if (num - last >= 1000)
			{
				seconds--;
				last = num;
				if (seconds < 0)
				{
					isFreez = false;
					seconds = 0;
				}
			}
			if (isTypeNewMod())
			{
				frame = hurt[GameCanvas.gameTick % hurt.Length];
			}
			else if (isNewModStand())
			{
				frame = attack1[GameCanvas.gameTick % attack1.Length];
			}
			else if (isNewMod())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					frame = 11;
				}
				else
				{
					frame = 10;
				}
			}
			else if (isSpecial())
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					frame = 1;
				}
				else
				{
					frame = 15;
				}
			}
			else if (GameCanvas.gameTick % 20 > 5)
			{
				frame = 11;
			}
			else
			{
				frame = 10;
			}
		}
		if (!isUpdate())
		{
			return;
		}
		if (isShadown)
		{
			updateShadown();
		}
		if (vMobMove == null && arrMobTemplate[templateId].rangeMove != 0)
		{
			return;
		}
		if (status != 3 && isBusyAttackSomeOne)
		{
			if (cFocus != null)
			{
				cFocus.doInjure(dame, dameMp, isCrit: false, isMob: true);
			}
			else if (mobToAttack != null)
			{
				mobToAttack.setInjure();
			}
			isBusyAttackSomeOne = false;
		}
		if (levelBoss > 0)
		{
			updateSuperEff();
		}
		switch (status)
		{
		case 1:
			isDisable = false;
			isDontMove = false;
			isFire = false;
			isIce = false;
			isWind = false;
			y += p1;
			if (GameCanvas.gameTick % 2 == 0)
			{
				if (p2 > 1)
				{
					p2--;
				}
				else if (p2 < -1)
				{
					p2++;
				}
			}
			x += p2;
			if (isTypeNewMod())
			{
				frame = hurt[GameCanvas.gameTick % hurt.Length];
			}
			else if (isNewModStand())
			{
				frame = attack1[GameCanvas.gameTick % attack1.Length];
			}
			else if (isNewMod())
			{
				frame = 11;
			}
			else if (isSpecial())
			{
				frame = 15;
			}
			else
			{
				frame = 11;
			}
			if (isDie)
			{
				isDie = false;
				if (isMobMe)
				{
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						if (((Mob)GameScr.vMob.elementAt(j)).mobId == mobId)
						{
							GameScr.vMob.removeElementAt(j);
						}
					}
				}
				p1 = 0;
				p2 = 0;
				x = (y = 0);
				hp = getTemplate().hp;
				status = 0;
				timeStatus = 0;
				break;
			}
			if ((TileMap.tileTypeAtPixel(x, y) & 2) == 2)
			{
				p1 = ((p1 <= 4) ? (-p1) : (-4));
				if (p3 == 0)
				{
					p3 = 16;
				}
			}
			else
			{
				p1++;
			}
			if (p3 > 0)
			{
				p3--;
				if (p3 == 0)
				{
					isDie = true;
				}
			}
			break;
		case 2:
			if (holdEffID == 0 && !isFreez && !blindEff && !sleepEff)
			{
				timeStatus = 0;
				updateMobStandWait();
			}
			break;
		case 4:
			if (holdEffID == 0 && !blindEff && !sleepEff && !isFreez)
			{
				timeStatus = 0;
				p1++;
				if (p1 > 40 + mobId % 5)
				{
					y -= 2;
					status = 5;
					p1 = 0;
				}
			}
			break;
		case 3:
			if (holdEffID == 0 && !blindEff && !sleepEff && !isFreez)
			{
				updateMobAttack();
			}
			break;
		case 5:
			if (holdEffID != 0 || blindEff || sleepEff)
			{
				break;
			}
			if (isFreez)
			{
				if (arrMobTemplate[templateId].type == 4)
				{
					ty++;
					wt++;
					fy += ((!wy) ? 1 : (-1));
					if (wt == 10)
					{
						wt = 0;
						wy = !wy;
					}
				}
			}
			else
			{
				timeStatus = 0;
				updateMobWalk();
			}
			break;
		case 6:
			timeStatus = 0;
			p1++;
			y += p1;
			if (y >= yFirst)
			{
				y = yFirst;
				p1 = 0;
				status = 5;
			}
			break;
		case 7:
			updateInjure();
			break;
		}
	}

	public void setInjure()
	{
		if (hp > 0 && status != 3)
		{
			timeStatus = 4;
			status = 7;
			if (getTemplate().type != 0 && Res.abs(x - xFirst) < 30)
			{
				x -= 10 * dir;
			}
		}
	}

	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss2)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BachTuoc)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt(idBoss);
		if (mob is NewBoss)
		{
			return (NewBoss)mob;
		}
		return null;
	}

	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				GameScr.vMob.removeElement(mob);
				break;
			}
		}
	}

	public void setAttack(Char cFocus)
	{
		isBusyAttackSomeOne = true;
		mobToAttack = null;
		this.cFocus = cFocus;
		p1 = 0;
		p2 = 0;
		status = 3;
		tick = 0;
		dir = ((cFocus.cx > x) ? 1 : (-1));
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		if (Res.abs(cx - x) < w * 2 && Res.abs(cy - y) < h * 2)
		{
			if (x < cx)
			{
				x = cx - w;
			}
			else
			{
				x = cx + w;
			}
			p3 = 0;
		}
		else
		{
			p3 = 1;
		}
	}

	private bool isSpecial()
	{
		if ((templateId >= 58 && templateId <= 65) || templateId == 67 || templateId == 68)
		{
			return true;
		}
		return false;
	}

	private bool isNewMod()
	{
		if (templateId >= 73 && !isNewModStand())
		{
			return true;
		}
		return false;
	}

	private bool isNewModStand()
	{
		if (templateId == 76)
		{
			return true;
		}
		return false;
	}

	private void updateInjure()
	{
		if (!isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
		{
			if (isTypeNewMod())
			{
				frame = hurt[GameCanvas.gameTick % hurt.Length];
			}
			else if (isNewModStand())
			{
				frame = attack1[GameCanvas.gameTick % attack1.Length];
			}
			else if (isNewMod())
			{
				if (frame != 10)
				{
					frame = 10;
				}
				else
				{
					frame = 11;
				}
			}
			else if (isSpecial())
			{
				if (frame != 1)
				{
					frame = 1;
				}
				else
				{
					frame = 15;
				}
			}
			else if (frame != 10)
			{
				frame = 10;
			}
			else
			{
				frame = 11;
			}
		}
		timeStatus--;
		if (timeStatus <= 0 && (isTypeNewMod() || isNewModStand() || (isNewMod() && frame == 11) || (isSpecial() && frame == 15) || (templateId < 58 && frame == 11)))
		{
			if ((injureBy != null && injureThenDie) || hp == 0)
			{
				status = 1;
				p2 = injureBy.cdir << 1;
				p1 = -3;
				p3 = 0;
			}
			else
			{
				status = 5;
				if (injureBy != null)
				{
					dir = -injureBy.cdir;
					if (Res.abs(x - injureBy.cx) < 24)
					{
						status = 2;
					}
				}
				p1 = (p2 = (p3 = 0));
				timeStatus = 0;
			}
			injureBy = null;
		}
		else if (arrMobTemplate[templateId].type != 0 && injureBy != null)
		{
			int num = -injureBy.cdir << 1;
			if (x > xFirst - arrMobTemplate[templateId].rangeMove && x < xFirst + arrMobTemplate[templateId].rangeMove)
			{
				x -= num;
			}
		}
	}

	private void updateMobStandWait()
	{
		checkFrameTick(stand);
		switch (arrMobTemplate[templateId].type)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			p1++;
			if (p1 > 10 + mobId % 10 && (cFocus == null || Res.abs(cFocus.cx - x) > 80) && (mobToAttack == null || Res.abs(mobToAttack.x - x) > 80))
			{
				status = 5;
			}
			break;
		case 4:
		case 5:
			p1++;
			if (p1 > mobId % 3 && (cFocus == null || Res.abs(cFocus.cx - x) > 80) && (mobToAttack == null || Res.abs(mobToAttack.x - x) > 80))
			{
				status = 5;
			}
			break;
		}
		if (cFocus != null && GameCanvas.gameTick % (10 + p1 % 20) == 0)
		{
			if (cFocus.cx > x)
			{
				dir = 1;
			}
			else
			{
				dir = -1;
			}
		}
		else if (mobToAttack != null && GameCanvas.gameTick % (10 + p1 % 20) == 0)
		{
			if (mobToAttack.x > x)
			{
				dir = 1;
			}
			else
			{
				dir = -1;
			}
		}
		if (forceWait > 0)
		{
			forceWait--;
			status = 2;
		}
	}

	public void updateMobAttack()
	{
		int[] array = ((p3 != 0) ? attack2 : attack1);
		if (tick < array.Length)
		{
			checkFrameTick(array);
			if (x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w && p3 == 0 && GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().charPunch(isKick: false, 0.05f);
			}
		}
		if (p1 == 0)
		{
			int num = 0;
			int num2 = 0;
			num = ((cFocus == null) ? mobToAttack.x : cFocus.cx);
			num2 = ((cFocus == null) ? mobToAttack.y : cFocus.cy);
			if (!isNewMod())
			{
				if (x > xFirst + arrMobTemplate[templateId].rangeMove)
				{
					p1 = 1;
				}
				if (x < xFirst - arrMobTemplate[templateId].rangeMove)
				{
					p1 = 1;
				}
			}
			if ((arrMobTemplate[templateId].type == 4 || arrMobTemplate[templateId].type == 5) && !isDontMove)
			{
				y += (num2 - y) / 20;
			}
			p2++;
			if (p2 > array.Length - 1 || p1 == 1)
			{
				p1 = 1;
				if (p3 == 0)
				{
					if (cFocus != null)
					{
						cFocus.doInjure(dame, dameMp, isCrit: false, isMob: true);
					}
					else
					{
						mobToAttack.setInjure();
					}
					isBusyAttackSomeOne = false;
				}
				else
				{
					if (cFocus != null)
					{
						MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), dame, dameMp, cFocus, getTemplate().dartType);
					}
					else
					{
						Char @char = new Char();
						@char.cx = mobToAttack.x;
						@char.cy = mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), dame, dameMp, @char, getTemplate().dartType);
					}
					isBusyAttackSomeOne = false;
				}
			}
			dir = ((x < num) ? 1 : (-1));
		}
		else if (p1 == 1)
		{
			if (arrMobTemplate[templateId].type != 0 && !isDontMove && !isIce && !isWind)
			{
				x += (xFirst - x) / 4;
				y += (yFirst - y) / 4;
			}
			if (Res.abs(xFirst - x) < 5 && Res.abs(yFirst - y) < 5 && tick == array.Length)
			{
				status = 2;
				p1 = 0;
				p2 = 0;
				tick = 0;
			}
		}
	}

	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			if (injureThenDie)
			{
				status = 1;
				p2 = injureBy.cdir << 3;
				p1 = -5;
				p3 = 0;
			}
			num = 1;
			if (isIce)
			{
				return;
			}
			if (isDontMove || isWind)
			{
				checkFrameTick(stand);
				return;
			}
			switch (arrMobTemplate[templateId].type)
			{
			case 0:
				if (isNewModStand())
				{
					frame = stand[GameCanvas.gameTick % stand.Length];
				}
				else
				{
					frame = 0;
				}
				num = 2;
				break;
			case 1:
			case 2:
			case 3:
			{
				num = 3;
				sbyte b = arrMobTemplate[templateId].speed;
				if (b == 1)
				{
					if (GameCanvas.gameTick % 2 == 1)
					{
						break;
					}
				}
				else if (b > 2)
				{
					b = (sbyte)(b + (sbyte)(mobId % 2));
				}
				else if (GameCanvas.gameTick % 2 == 1)
				{
					b = (sbyte)(b - 1);
				}
				x += b * dir;
				if (x > xFirst + arrMobTemplate[templateId].rangeMove)
				{
					dir = -1;
				}
				else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
				{
					dir = 1;
				}
				if (Res.abs(x - Char.myCharz().cx) < 40 && Res.abs(x - xFirst) < arrMobTemplate[templateId].rangeMove)
				{
					dir = ((x <= Char.myCharz().cx) ? 1 : (-1));
					if (Res.abs(x - Char.myCharz().cx) < 20)
					{
						x -= dir * 10;
					}
					status = 2;
					forceWait = 20;
				}
				checkFrameTick((w <= 30) ? moveFast : move);
				break;
			}
			case 4:
			{
				num = 4;
				sbyte speed2 = arrMobTemplate[templateId].speed;
				speed2 = (sbyte)(speed2 + (sbyte)(mobId % 2));
				x += speed2 * dir;
				if (GameCanvas.gameTick % 10 > 2)
				{
					y += speed2 * dirV;
				}
				speed2 = (sbyte)(speed2 + (sbyte)((GameCanvas.gameTick + mobId) % 2));
				if (x > xFirst + arrMobTemplate[templateId].rangeMove)
				{
					dir = -1;
					status = 2;
					forceWait = GameCanvas.gameTick % 20 + 20;
					p1 = 0;
				}
				else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
				{
					dir = 1;
					status = 2;
					forceWait = GameCanvas.gameTick % 20 + 20;
					p1 = 0;
				}
				if (y > yFirst + 24)
				{
					dirV = -1;
				}
				else if (y < yFirst - (20 + GameCanvas.gameTick % 10))
				{
					dirV = 1;
				}
				checkFrameTick(move);
				break;
			}
			case 5:
			{
				num = 5;
				sbyte speed = arrMobTemplate[templateId].speed;
				speed = (sbyte)(speed + (sbyte)(mobId % 2));
				x += speed * dir;
				speed = (sbyte)(speed + (sbyte)((GameCanvas.gameTick + mobId) % 2));
				if (GameCanvas.gameTick % 10 > 2)
				{
					y += speed * dirV;
				}
				if (x > xFirst + arrMobTemplate[templateId].rangeMove)
				{
					dir = -1;
					status = 2;
					forceWait = GameCanvas.gameTick % 20 + 20;
					p1 = 0;
				}
				else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
				{
					dir = 1;
					status = 2;
					forceWait = GameCanvas.gameTick % 20 + 20;
					p1 = 0;
				}
				if (y > yFirst + 24)
				{
					dirV = -1;
				}
				else if (y < yFirst - (20 + GameCanvas.gameTick % 10))
				{
					dirV = 1;
				}
				if (TileMap.tileTypeAt(x, y, 2))
				{
					if (GameCanvas.gameTick % 10 > 5)
					{
						y = TileMap.tileYofPixel(y);
						status = 4;
						p1 = 0;
						dirV = -1;
					}
					else
					{
						dirV = -1;
					}
				}
				break;
			}
			}
		}
		catch (Exception)
		{
			Cout.println("lineee: " + num);
		}
	}

	public MobTemplate getTemplate()
	{
		return arrMobTemplate[templateId];
	}

	public bool isPaint()
	{
		if (x < GameScr.cmx)
		{
			return false;
		}
		if (x > GameScr.cmx + GameScr.gW)
		{
			return false;
		}
		if (y < GameScr.cmy)
		{
			return false;
		}
		if (y > GameScr.cmy + GameScr.gH + 30)
		{
			return false;
		}
		if (arrMobTemplate[templateId] == null)
		{
			return false;
		}
		if (arrMobTemplate[templateId].data == null)
		{
			return false;
		}
		if (arrMobTemplate[templateId].data.img == null)
		{
			return false;
		}
		if (status == 0)
		{
			return false;
		}
		return true;
	}

	public bool isUpdate()
	{
		if (arrMobTemplate[templateId] == null)
		{
			return false;
		}
		if (arrMobTemplate[templateId].data == null)
		{
			return false;
		}
		if (status == 0)
		{
			return false;
		}
		return true;
	}

	public bool checkIsBoss()
	{
		if (isBoss || levelBoss > 0)
		{
			return true;
		}
		return false;
	}

	public virtual void paint(mGraphics g)
	{
		if (isShadown && status != 0)
		{
			paintShadow(g);
		}
		if (!isPaint() || (status == 1 && p3 > 0 && GameCanvas.gameTick % 3 == 0))
		{
			return;
		}
		g.translate(0, GameCanvas.transY);
		if (!changBody)
		{
			arrMobTemplate[templateId].data.paintFrame(g, frame, x, y + fy, (dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, smallBody, x, y + fy - 14, 0, 3);
		}
		g.translate(0, -GameCanvas.transY);
		if (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.Equals(this) && status != 1)
		{
			int num = (int)((long)hp * 100L / maxHp) / 10 - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 9)
			{
				num = 9;
			}
			g.drawRegion(imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y - h - 10, 3);
		}
	}

	public int getHPColor()
	{
		return 16711680;
	}

	public void startDie()
	{
		hp = 0;
		injureThenDie = true;
		hp = 0;
		status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		p1 = -3;
		p2 = -dir;
		p3 = 0;
	}

	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		isBusyAttackSomeOne = true;
		cFocus = null;
		p1 = 0;
		p2 = 0;
		status = 3;
		tick = 0;
		dir = ((mobToAttack.x > x) ? 1 : (-1));
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		if (Res.abs(num - x) < w * 2 && Res.abs(num2 - y) < h * 2)
		{
			if (x < num)
			{
				x = num - w;
			}
			else
			{
				x = num + w;
			}
			p3 = 0;
		}
		else
		{
			p3 = 1;
		}
	}

	public int getX()
	{
		return x;
	}

	public int getY()
	{
		return y;
	}

	public int getH()
	{
		return h;
	}

	public int getW()
	{
		return w;
	}

	public void stopMoving()
	{
		if (status == 5)
		{
			status = 2;
			p1 = (p2 = (p3 = 0));
			forceWait = 50;
		}
	}

	public bool isInvisible()
	{
		return status == 0 || status == 1;
	}

	public void removeHoldEff()
	{
		if (holdEffID != 0)
		{
			holdEffID = 0;
		}
	}

	public void removeBlindEff()
	{
		blindEff = false;
	}

	public void removeSleepEff()
	{
		sleepEff = false;
	}

	public void GetFrame()
	{
		if (isGetFr && isTypeNewMod() && arrMobTemplate[templateId].data != null)
		{
			frameArr = (int[][])Controller.frameHT_NEWBOSS.get(templateId + string.Empty);
			stand = frameArr[0];
			move = frameArr[1];
			moveFast = frameArr[2];
			attack1 = frameArr[3];
			attack2 = frameArr[4];
			hurt = frameArr[5];
			isGetFr = false;
		}
	}

	private bool isTypeNewMod()
	{
		if (arrMobTemplate[templateId].data != null && arrMobTemplate[templateId].data.typeData == 2)
		{
			return true;
		}
		return false;
	}
}
