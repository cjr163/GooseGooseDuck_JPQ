using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggd
{
    public class game_base
    {
        /// <summary>
        /// 阵营
        /// </summary>
        public enum ENUM_Camps
        {
            Bird,  
            Goose, 
            Duck,  
        }

        public struct ST_Camp_name
        {
            /// <summary>
            /// 中立 Bird
            /// </summary>
            public string Neutral;
            /// <summary>
            /// 正派 Goose
            /// </summary>
            public string Positive;
            /// <summary>
            /// 反派 Duck
            /// </summary>
            public string Villain;
        }
        /// <summary>
        /// 阵营
        /// </summary>
        public static ST_Camp_name Camps;

        /// <summary>
        /// 中立*7 Neutral
        /// </summary>
        public enum ENUM_Bird_Roles
        {
            呆呆鸟,
            决斗呆呆,
            秃鹫,
            鸽子,
            鹈鹕,
            猎鹰,
            恋人, //x2 复选框
        }

        /// <summary>
        /// 正派*20 Positive
        /// </summary>
        public enum ENUM_Goose_Roles
        {
            肉汁,
            通灵,
            正义,
            警长,
            加拿大,
            模仿者,
            侦探,
            观鸟,
            政治家,
            锁匠,
            殡仪员,
            网红,
            冒险家,
            复仇者,
            星界,
            工程师,
            流浪儿童,
            追踪,
            跟踪者,
            保镖,
            //超能力鹅,
        }

        /// <summary>
        /// 反派*17  Villain
        /// </summary>
        public enum ENUM_Duck_Roles
        {
            雇佣杀手,
            食鸟鸭,
            变形者,
            静语者,
            专业杀手,
            间牒,
            刺客,
            告密者,
            派对狂,
            炸弹王,
            身份窃贼,
            忍者,
            丧葬者,
            隐形,
            连环杀手,
            术士,
            超能力鸭,
        }


        /// <summary>
        /// 地图
        /// </summary>
        public enum ENUM_Maps
        {
            地下室,
            从林神殿,
            马拉德庄园,
            连结殖民地,
            黑天鹅,
            老妈鹅星球飞船,
            鹅教堂,
            古代沙地
        }
        /// <summary>
        /// 地图角色属性
        /// </summary>
        public struct ST_role_attribute
        {
            /// <summary>
            /// 索引i
            /// </summary>
            public int index;
            /// <summary>
            /// 角色名
            /// </summary>
            public string name;
            /// <summary>
            /// 阵营
            /// </summary>
            public string camp;
            /// <summary>
            /// 游戏设定角色
            /// </summary>
            public bool game_check;
        }
        public struct ST_map
        {
            /// <summary>
            /// 索引i
            /// </summary>
            public int index;
            public string name;
            public List<ST_role_attribute> roles_List;
        }
        /// <summary>
        /// 基础数据表
        /// </summary>
        public List<ST_map> Map_List;


        /// <summary>
        /// 构造函数
        /// </summary>
        public game_base() 
        {
            Camps = new ST_Camp_name();
            Camps.Neutral = Enum.GetName(typeof(ENUM_Camps), ENUM_Camps.Bird);
            Camps.Positive = Enum.GetName(typeof(ENUM_Camps), ENUM_Camps.Goose);
            Camps.Villain = Enum.GetName(typeof(ENUM_Camps), ENUM_Camps.Duck);

            List <ST_role_attribute>  all_role = new List<ST_role_attribute>();   //所有角色属性表

            string[] names = Enum.GetNames(typeof(ENUM_Bird_Roles));    //角色名
            int index =0;
            foreach(var n in names)
            {
                all_role.Add(new ST_role_attribute { index = index++ , name = n, camp = Camps.Neutral, game_check = true }) ;
            }

            names = Enum.GetNames(typeof(ENUM_Goose_Roles));
            foreach(var n in names)
            {
                all_role.Add(new ST_role_attribute { index = index++, name = n, camp = Camps.Positive, game_check = true });
            }

            names = Enum.GetNames(typeof(ENUM_Duck_Roles));
            foreach(var n in names)
            {
                all_role.Add(new ST_role_attribute { index = index++, name = n, camp = Camps.Villain, game_check = true });
            }

            //各图
            Map_List = new List<ST_map>();

            for(int i = 0;i < Enum.GetValues(typeof(ENUM_Maps)).Length;i++)
            {
                Map_List.Add(
                    new ST_map
                    {
                        index =i,
                        name = Enum.GetName(typeof(ENUM_Maps), i),
                        roles_List = all_role.ToList(),
                    }
                );
            }

            //各图默认缺少角色
            string[] del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.猎鹰),
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.秃鹫),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.模仿者),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.冒险家),                
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.变形者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            ST_map m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.地下室));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.工程师),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.食鸟鸭),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.变形者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.从林神殿));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.身份窃贼),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.丧葬者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.马拉德庄园));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles), ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.观鸟),
                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.冒险家),
                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles), ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles), ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles), ENUM_Duck_Roles.身份窃贼),
                Enum.GetName(typeof(ENUM_Duck_Roles), ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles), ENUM_Duck_Roles.丧葬者),
                Enum.GetName(typeof(ENUM_Duck_Roles), ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.连结殖民地));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.冒险家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.复仇者),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.身份窃贼),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.丧葬者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.黑天鹅));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.猎鹰),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.冒险家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.身份窃贼),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.丧葬者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.老妈鹅星球飞船));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.冒险家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.流浪儿童),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.追踪),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.身份窃贼),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.忍者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.丧葬者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.术士),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.鹅教堂));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }


            del = new string[]
            {
                Enum.GetName(typeof(ENUM_Bird_Roles),ENUM_Bird_Roles.鹈鹕),

                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.政治家),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.锁匠),
                Enum.GetName(typeof(ENUM_Goose_Roles),ENUM_Goose_Roles.工程师),

                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.告密者),
                Enum.GetName(typeof(ENUM_Duck_Roles),ENUM_Duck_Roles.身份窃贼),
            };
            m = Map_List.Single(a => a.name == Enum.GetName(typeof(ENUM_Maps), ENUM_Maps.古代沙地));
            foreach(var s in del)
            {
                var aaa = m.roles_List.Single(a => a.name == s);
                aaa.game_check = false;
                m.roles_List[aaa.index] = aaa;
            }

        }
    }
}
