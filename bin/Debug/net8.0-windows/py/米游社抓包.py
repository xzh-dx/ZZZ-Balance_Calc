import requests
import re
import json
import webbrowser
import pyperclip
from collections import Counter

def 取角色uid(cookies):
    headers = {
        'Host': 'api-takumi.mihoyo.com',
        'Connection': 'keep-alive',
        'Accept': 'application/json, text/plain, */*',
        'User-Agent': 'Mozilla/5.0 (Linux; Android 9; 23113RKC6C Build/PQ3A.190605.06200901; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/91.0.4472.114 Mobile Safari/537.36 miHoYoBBS/2.75.2',
        'Origin': 'https://act.mihoyo.com',
        'X-Requested-With': 'com.mihoyo.hyperion',
        'Sec-Fetch-Site': 'same-site',
        'Sec-Fetch-Mode': 'cors',
        'Sec-Fetch-Dest': 'empty',
        'Referer': 'https://act.mihoyo.com/',
        'Accept-Language': 'zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7',
    }
    params = {'game_biz': 'nap_cn',}
    response = requests.get('https://api-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie',params=params,cookies=cookies,headers=headers,verify=False)
    json = response.json()
    uid = json['data']['list'][0]['game_uid']
    print(f"获取到的角色uid为{uid}")
    return uid

def 取角色cookies():
    webbrowser.open('http://user.mihoyo.com/')
    cookies = {}
    文本 = r"var cookie=document.cookie;var ask=confirm('Cookie:'+cookie+'\n\nDo you want to copy the cookie to the clipboard?');if(ask==true){copy(cookie);msg=cookie}else{msg='Cancel'}"
    pyperclip.copy(文本)
    cookie_文本 = input('请粘贴浏览器返回的字符并按回车:')
    for cookie in cookie_文本.split(';'):
        key, value = cookie.strip().split('=')
        cookies[key] = value
    return cookies

def 取角色列表id(uid, cookies):
    headers = {
        'Host': 'api-takumi-record.mihoyo.com',
        'Connection': 'keep-alive',
        'x-rpc-platform': '2',
        'x-rpc-geetest_ext': '{"viewUid":"0","gameId":8,"page":"v1.1.4_#/zzz/roles/all","isHost":1}',
        'x-rpc-app_version': '2.75.2',
        'x-rpc-language': 'zh-cn',
        'User-Agent': 'Mozilla/5.0 (Linux; Android 9; 23113RKC6C Build/PQ3A.190605.06200901; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/91.0.4472.114 Mobile Safari/537.36 miHoYoBBS/2.75.2',
        'x-rpc-device_id': '06770e63-c0e8-38da-89bd-1a1e504b6bfd',
        'Accept': 'application/json, text/plain, */*',
        'x-rpc-device_name': 'Redmi%2023113RKC6C',
        'x-rpc-page': 'v1.1.4_#/zzz/roles/all',
        'x-rpc-device_fp': '38d7fe73b1032',
        'x-rpc-lang': 'zh-cn',
        'x-rpc-sys_version': '9',
        'Origin': 'https://act.mihoyo.com',
        'X-Requested-With': 'com.mihoyo.hyperion',
        'Sec-Fetch-Site': 'same-site',
        'Sec-Fetch-Mode': 'cors',
        'Sec-Fetch-Dest': 'empty',
        'Referer': 'https://act.mihoyo.com/',
        'Accept-Language': 'zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7',
     }
    params = {'server': 'prod_gf_cn','role_id': uid,}
    response = requests.get('https://api-takumi-record.mihoyo.com/event/game_record_zzz/api/zzz/avatar/basic', params = params, headers = headers, cookies = cookies, verify = False)
    res = response.json()
    角色id字典 = {}
    for jso in res['data']['avatar_list']:
        键 = ''.join(re.findall(r'[\u4e00-\u9fff0-9]+', str(jso['full_name_mi18n'])))
        值 = str(jso['id'])
        角色id字典[键] = 值
    #print(f"角色id和字典: {角色id字典}")
    return 角色id字典

def 取角色装备(cookies, 角色id, uid):
    print(角色id)
    headers = {
        'Host': 'api-takumi-record.mihoyo.com',
        'Connection': 'keep-alive',
        'x-rpc-platform': '2',
        'x-rpc-geetest_ext': f'{{"viewUid":"0","gameId":8,"page":"v1.1.4_#/zzz/roles/{角色id}/detail","isHost":1}}',
        'x-rpc-app_version': '2.75.2',
        'x-rpc-language': 'zh-cn',
        'User-Agent': 'Mozilla/5.0 (Linux; Android 9; 23113RKC6C Build/PQ3A.190605.06200901; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/91.0.4472.114 Mobile Safari/537.36 miHoYoBBS/2.75.2',
        'x-rpc-device_id': '06770e63-c0e8-38da-89bd-1a1e504b6bfd',
        'Accept': 'application/json, text/plain, */*',
        'x-rpc-device_name': 'Redmi%2023113RKC6C',
        'x-rpc-page': f'v1.1.4_#/zzz/roles/{角色id}/detail',
        'x-rpc-device_fp': '38d7fe73b1032',
        'x-rpc-lang': 'zh-cn',
        'x-rpc-sys_version': '9',
        'Origin': 'https://act.mihoyo.com',
        'X-Requested-With': 'com.mihoyo.hyperion',
        'Sec-Fetch-Site': 'same-site',
        'Sec-Fetch-Mode': 'cors',
        'Sec-Fetch-Dest': 'empty',
        'Referer': 'https://act.mihoyo.com/',
        'Accept-Language': 'zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7',
    }
    response = requests.get(f'https://api-takumi-record.mihoyo.com/event/game_record_zzz/api/zzz/avatar/info?id_list[]={角色id}&need_wiki=true&server=prod_gf_cn&role_id={uid}',cookies=cookies,headers=headers,verify=False)
    json = response.json()
    #print(f"获取的装备信息:{json}")
    return json

def 读取字典(字典名):
    with open(f"./json/{字典名}.txt", 'r', encoding='utf-8') as file:
        读取的库 = json.load(file)
    return 读取的库

def 保存字典(字典, 字典名):
    # 将字典转换为字符串并保存到文本文件
    with open(f"./save/{字典名}.txt", "w", encoding="utf-8") as file:
        json.dump(字典, file, ensure_ascii=False, indent=4)

def 转浮点数(值):
    if isinstance(值, float):
        return 值
    值 = re.sub(r'[^0-9.\%]', '', 值)
    try:
        if '%' in 值:
            数 = float(值.replace('%', '')) / 100
            数 = round(数, 3)
            return 数
        else:
            return round(float(值), 3)
    except ValueError:
        print(f"无法将字符串转换为浮点数: {值}")
        return 值

def 取驱动盘主属性返回字典(号, json):
    返回字典 = {}
    try:
        返回字典['驱动盘名'] = json['data']['avatar_list'][0]['equip'][号 - 1]['equip_suit']['name']
    except:
        返回字典['驱动盘名'] = '未佩戴驱动盘'
    返回字典['驱动盘号'] = 号
    if 号 == 1:
        驱动盘主属性 = '小生命值'
    elif 号 == 2:
        驱动盘主属性 = '小攻击力'
    elif 号 == 3:
        驱动盘主属性 = '小防御力'
    else:
        try:
            驱动盘主属性 = json['data']['avatar_list'][0]['equip'][号 - 1]['main_properties'][0]['property_name']
        except:
            驱动盘主属性 = '无'
    try:
        驱动盘主属性值 = 转浮点数(json['data']['avatar_list'][0]['equip'][号 - 1]['main_properties'][0]['base'])
    except:
        驱动盘主属性值 = 0
    返回字典[驱动盘主属性] = 驱动盘主属性值
    return 返回字典

def 取驱动盘随机属性返回字典(号, json):
    返回字典 = {}
    for i in [0,1,2,3]:
        try:
            驱动盘随机属性 = json['data']['avatar_list'][0]['equip'][号 - 1]['properties'][i]['property_name']
            驱动盘随机属性值 = 转浮点数(json['data']['avatar_list'][0]['equip'][号 - 1]['properties'][i]['base'])
            if 驱动盘随机属性 == '生命值':
                if 驱动盘随机属性值 > 1:
                    驱动盘随机属性 = '小生命值'
            if 驱动盘随机属性 == '攻击力':
                if 驱动盘随机属性值 > 1:
                    驱动盘随机属性 = '小攻击力'
            if 驱动盘随机属性 == '防御力':
                if 驱动盘随机属性值 > 1:
                    驱动盘随机属性 = '小防御力'
            返回字典[驱动盘随机属性] = 驱动盘随机属性值
        except:
            返回字典[f'{i + 1}无'] = 0
    return 返回字典

def 识别驱动盘两件套(字符串1, 字符串2, 字符串3, 字符串4, 字符串5, 字符串6):
    strings = [字符串1, 字符串2, 字符串3, 字符串4, 字符串5, 字符串6]
    套装 = Counter(strings)
    驱动盘两件套 = []
    索引 = 0
    for 键,值 in 套装.items():
        if 值 > 1:
            if 索引 >= len(驱动盘两件套):
                驱动盘两件套.extend([None] * (索引 + 1 - len(驱动盘两件套)))

            驱动盘两件套[索引] = 键  # 然后进行赋值
            索引 += 1
    return 驱动盘两件套

def 加载两件套属性库(两件套列表):
    两件套装数据 = 读取字典("驱动盘套装库")
    两件套属性 = {}
    for i in 两件套列表:
        if i == "未佩戴驱动盘":
            两件套属性.update({"无": 0})
            break
        两件套属性.update(两件套装数据[i])
    return 两件套属性

def 角色属性校正返回字典(角色属性, json):
    返回字典 = 角色属性
    for i in [0,1,2]:
        键 = json['data']['avatar_list'][0]['properties'][i]['property_name']
        值 = json['data']['avatar_list'][0]['properties'][i]
        if 值['base'] == "":
            返回字典[f"基础{键}"] = 转浮点数(值['final'])
        else:
            返回字典[f"基础{键}"] = 转浮点数(值['base'])
    return 返回字典

def 音擎属性校正返回字典(音擎属性, json):
    返回字典 = 音擎属性
    键 = json['data']['avatar_list'][0]['weapon']['properties'][0]['property_name']
    值 = 转浮点数(json['data']['avatar_list'][0]['weapon']['properties'][0]['base'])
    键2 = json['data']['avatar_list'][0]['weapon']['main_properties'][0]['property_name']
    值2 = 转浮点数(json['data']['avatar_list'][0]['weapon']['main_properties'][0]['base'])
    返回字典[键] = 值
    返回字典[键2] = 值2
    return 返回字典

def 获取核心技属性(json ,角色属性):
    返回字典 = {}
    核心技强化情况 = json['data']['avatar_list'][0]['skills'][4]['level'] - 1
    if 核心技强化情况 == 0:
        返回字典["无"] = 0
        返回字典["基础攻击力"] = 0
        return 返回字典
    映射关系 = {1: (1, 0),2: (1, 1),3: (2, 1),4: (2, 2),5: (3, 2),6: (3, 3)}
    A, B = 映射关系[核心技强化情况]
    键 = 角色属性['核心技属性']
    值 = 转浮点数(角色属性['核心技值']) * A
    返回字典[键] = round(值, 3)
    返回字典["基础攻击力"] = 25 * B
    return 返回字典

def main():
    角色列表 = 读取字典("角色库")
    音擎列表 = 读取字典("音擎库")
    cookies = 取角色cookies()
    uid = 取角色uid(cookies)
    角色id字典 = 取角色列表id(uid, cookies)
    for 键, 值 in 角色id字典.items():
        角色整体属性 = {}
        角色整体属性.clear()

        json = 取角色装备(cookies, 值, uid)
        角色属性 = 角色列表[键]
        角色属性 = 角色属性校正返回字典(角色属性, json)
        核心技属性 = 获取核心技属性(json, 角色属性)
        try:
            音擎名 = json['data']['avatar_list'][0]['weapon']['name']
            音擎名 = ''.join(re.findall(r'[\u4e00-\u9fff0-9]+', 音擎名))
            音擎属性 = 音擎列表[音擎名]
            音擎属性 = 音擎属性校正返回字典(音擎属性, json)
        except:
            音擎属性 = {"音擎名": "未佩戴音擎","基础攻击力": 0,"无": 0}
        角色属性["基础攻击力"] = 角色属性["基础攻击力"] - 音擎属性["基础攻击力"] - 核心技属性["基础攻击力"]

        驱动盘主属性字典1 = 取驱动盘主属性返回字典(1, json)
        驱动盘主属性字典2 = 取驱动盘主属性返回字典(2, json)
        驱动盘主属性字典3 = 取驱动盘主属性返回字典(3, json)
        驱动盘主属性字典4 = 取驱动盘主属性返回字典(4, json)
        驱动盘主属性字典5 = 取驱动盘主属性返回字典(5, json)
        驱动盘主属性字典6 = 取驱动盘主属性返回字典(6, json)

        驱动盘两件套 = 识别驱动盘两件套(驱动盘主属性字典1['驱动盘名'], 驱动盘主属性字典2['驱动盘名'], 驱动盘主属性字典3['驱动盘名'], 驱动盘主属性字典4['驱动盘名'], 驱动盘主属性字典5['驱动盘名'], 驱动盘主属性字典6['驱动盘名'])
        两件套属性 = 加载两件套属性库(驱动盘两件套)

        驱动盘随机属性字典1 = 取驱动盘随机属性返回字典(1, json)
        驱动盘随机属性字典2 = 取驱动盘随机属性返回字典(2, json)
        驱动盘随机属性字典3 = 取驱动盘随机属性返回字典(3, json)
        驱动盘随机属性字典4 = 取驱动盘随机属性返回字典(4, json)
        驱动盘随机属性字典5 = 取驱动盘随机属性返回字典(5, json)
        驱动盘随机属性字典6 = 取驱动盘随机属性返回字典(6, json)

        角色整体属性["角色属性"] = 角色属性
        角色整体属性["音擎属性"] = 音擎属性
        角色整体属性["两件套属性"] = 两件套属性
        角色整体属性["驱动盘主属性1"] = 驱动盘主属性字典1
        角色整体属性["驱动盘主属性2"] = 驱动盘主属性字典2
        角色整体属性["驱动盘主属性3"] = 驱动盘主属性字典3
        角色整体属性["驱动盘主属性4"] = 驱动盘主属性字典4
        角色整体属性["驱动盘主属性5"] = 驱动盘主属性字典5
        角色整体属性["驱动盘主属性6"] = 驱动盘主属性字典6
        角色整体属性["驱动盘随机属性1"] = 驱动盘随机属性字典1
        角色整体属性["驱动盘随机属性2"] = 驱动盘随机属性字典2
        角色整体属性["驱动盘随机属性3"] = 驱动盘随机属性字典3
        角色整体属性["驱动盘随机属性4"] = 驱动盘随机属性字典4
        角色整体属性["驱动盘随机属性5"] = 驱动盘随机属性字典5
        角色整体属性["驱动盘随机属性6"] = 驱动盘随机属性字典6
        角色整体属性["核心技属性"] = 核心技属性

        保存字典(角色整体属性, 键)
        print(键)

if __name__ == "__main__":
    main()