#UI

##1. Page Router
1. 通过队列管理页面
2. 传入Pages,其所有子节点是需要管理的页面
3. openPage 传入需要打开的页面在hierarchy中的名字
4. closePager
###1. Page prefab
1. 控制页面动画
2. 提供PageRouter接口
3. NotOpen>>(open true)>>OpenEnQueue >>            >>Opened  >> CloseEnQueue>> closed
   NotOpen<<           <<ColseDeQueue<<(open false)<<Opened  << OpenDeQueue << closed

##2. Start UI
1. Start

    loadScene
2. Setting

    checkbox  slider
    1. background volume
    2. game volume
    3. back
3. Quit

##3. Main Game UI
1. score
2. health
3. skills
    1. 普攻 mouse 0  消耗bullet
    2. 炸弹 q （通过敌机获得） 
    3. 护盾 e （通过敌机获得） （防实体弹， 激光攻击削弱， 炸弹破盾并无减伤）
    4. 迁跃 space （消耗燃料，通过敌机获得）
4. weapons（可以通过歼灭敌机获得）
    1. 普通 子弹无限
    2. 霰弹
    3. ray
    4. chaser （信号弹，标记敌机，几秒内引导子弹攻击并重伤）
    
##4. Pause UI
在Main Game UI中
### 1. 恢复
### 2. 重新开始
### 3. Start UI （回到首页）


#2.Game Logic
##1. Manager 都用单例
###1. AudioManager
###2. PoolManager

##2. Controller
###1. GameController
###2. EnemyController
###3. UIController

##3. EnemiesSpawn


#1. 第一阶段 Player基本实现
Player类 封装属性方法
PlayerController类 操作控制
Player上绑定所有技能，在PlayerController类中控制
Player子节点有所有武器，拖动赋值给Shoot技能

碰撞关系
player-enemy 两层


#1. 已实现内容
1. PoolManager
2. Player Control
3. Explosion
4. Bullet
5. Weapons
6. Skills

#2. 剩余主要内容
1. Enemy AI
2. Enemy Spawn
3. Item : health rock
3. Game Process

4. Skill UI             ✔
5. UI Page Router
6. UI Page

7. AudioManager
8. All Audio
9. Weapon UI            ✔



