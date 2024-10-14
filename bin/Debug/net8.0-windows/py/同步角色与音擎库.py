import requests
import re
import json
from bs4 import BeautifulSoup

def 保存字典(字典, 字典名):
    # 将字典转换为字符串并保存到文本文件
    with open(f"./json/{字典名}.txt", "w", encoding="utf-8") as file:
        json.dump(字典, file, ensure_ascii=False, indent=4)

def 转浮点数(值, 校验 = 999.0):
    try:
        if '%' in 值:
            数 = float(值.replace('%', '')) / 100
            数 = round(数, 3)
            if 数 > 校验:
                数 = 数 / 10
            return 数
        else:
            return float(值)
    except ValueError:
        return 值

角色属性 = ["角色名","角色id","基础生命值","基础攻击力","基础防御力","基础冲击力","暴击率","暴击伤害","基础异常掌控","异常精通","穿透率","基础能量自动回复","稀有度","角色属性","角色特性","阵营"]
角色和id = {}
音擎和id = {}
驱动盘和id = {}
角色库 = {}
音擎库 = {}
驱动盘库 = {}

headers = {
    'accept': 'application/json, text/plain, */*',
    'accept-language': 'zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6,zh-TW;q=0.5',
    'origin': 'https://baike.mihoyo.com',
    'priority': 'u=1, i',
    'referer': 'https://baike.mihoyo.com/',
    'sec-ch-ua': '"Chromium";v="128", "Not;A=Brand";v="24", "Microsoft Edge";v="128"',
    'sec-ch-ua-mobile': '?0',
    'sec-ch-ua-platform': '"Windows"',
    'sec-fetch-dest': 'empty',
    'sec-fetch-mode': 'cors',
    'sec-fetch-site': 'same-site',
    'user-agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0',
}
params = {
    'app_sn': 'zzz_wiki',
    'channel_id': '2',
}
response = requests.get(
    'https://api-takumi-static.mihoyo.com/common/blackboard/zzz_wiki/v1/home/content/list',
    params=params,
    headers=headers,
)
jso = response.json()

for j in jso['data']['list'][0]['children'][0]['list']:
    键 = ''.join(re.findall(r'[\u4e00-\u9fff0-9]+', str(j['title'])))
    值 = str(j['content_id'])
    角色和id.update({键 : 值})
print('角色列表已更新完成')

for j in jso['data']['list'][0]['children'][1]['list']:
    键 = ''.join(re.findall(r'[\u4e00-\u9fff0-9]+', str(j['title'])))
    值 = str(j['content_id'])
    音擎和id.update({键 : 值})
print('音擎列表已更新完成')

for j in jso['data']['list'][0]['children'][3]['list']:
    键 = ''.join(re.findall(r'[\u4e00-\u9fff0-9]+', str(j['title'])))
    值 = str(j['content_id'])
    驱动盘和id.update({键 : 值})
print('驱动盘列表已更新完成')

headers = {
    'accept': 'application/json, text/plain, */*',
    'accept-language': 'zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6,zh-TW;q=0.5',
    'origin': 'https://baike.mihoyo.com',
    'priority': 'u=1, i',
    'referer': 'https://baike.mihoyo.com/',
    'sec-ch-ua': '"Chromium";v="128", "Not;A=Brand";v="24", "Microsoft Edge";v="128"',
    'sec-ch-ua-mobile': '?0',
    'sec-ch-ua-platform': '"Windows"',
    'sec-fetch-dest': 'empty',
    'sec-fetch-mode': 'cors',
    'sec-fetch-site': 'same-site',
    'user-agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0',
    'x-rpc-wiki_app': 'zzz',
}

def 更新音擎库():
    for 键,值 in 音擎和id.items():
        音擎库[键] = {}
        params = {
            'app_sn': 'zzz_wiki',
            'entry_page_id': 值,
            'lang': 'zh-cn',
        }
        音擎库[键].update({"音擎名": 键})
        音擎库[键].update({"音擎id": 值})
        response = requests.get('https://api-takumi-static.mihoyo.com/hoyowiki/zzz/wapi/entry_page', params=params, headers=headers)
        jso = response.json()
        js = json.loads(jso['data']['page']['modules'][4]['components'][0]['data'])
        js = re.search(r'满级面板：(.*?)</p>', str(js)).group(1)
        parts = js.split(' ', 1)

        value = re.search(r'''\d+''', parts[0]).group(0)
        value = 转浮点数(value)
        音擎库[键].update({"基础攻击力" : value})

        value = re.search(r'''[\u4e00-\u9fa5]+''', parts[1]).group(0)
        value2 = re.search(r'''(?<=\+)[\d%]+''', parts[1]).group(0)
        value2 = 转浮点数(value2)
        音擎库[键].update({value : value2})
        print(f'{键}属性已更新')
    print('音擎库已更新完成')

def 更新角色库():
    for 键,值 in 角色和id.items():
        角色库[键] = {}
        params = {
            'app_sn': 'zzz_wiki',
            'entry_page_id': 值,
            'lang': 'zh-cn',
        }

        角色库[键].update({角色属性[0]: 键})
        角色库[键].update({角色属性[1]: 值})
        response = requests.get('https://api-takumi-static.mihoyo.com/hoyowiki/zzz/wapi/entry_page', params=params, headers=headers)
        jso = response.json()
        js = json.loads(jso['data']['page']['modules'][3]['components'][0]['data'])
        i = 2
        for j in js['list'][0]['attr']:
            value = re.search(r'>(\d+\.?\d*%?)<', j['value'])
            value = value.group(1)
            value = 转浮点数(value)
            角色库[键].update({角色属性[i] : value})
            i += 1
        i = 2
        for j in js['list'][6]['attr']:
            if i == 5:
                break
            value = re.search(r'>(\d+\.?\d*%?)<', j['value'])
            value = value.group(1)
            value = 转浮点数(value)
            角色库[键].update({角色属性[i]: value})
            i += 1

        js = json.loads(jso['data']['page']['ext']['fe_ext'])
        j = js['c_43']['filter']['text']
        jj = json.loads(j)
        i = 12
        for j in jj:
            value = re.search(r'/([^/]+)', j)
            if i == 13:
                if value.group(1) == "火":
                    角色库[键].update({角色属性[i]: "火属性伤害加成"})
                elif value.group(1) == "冰":
                    角色库[键].update({角色属性[i]: "冰属性伤害加成"})
                elif value.group(1) == "电":
                    角色库[键].update({角色属性[i]: "电属性伤害加成"})
                elif value.group(1) == "物理":
                    角色库[键].update({角色属性[i]: "物理伤害加成"})
                elif value.group(1) == "以太":
                    角色库[键].update({角色属性[i]: "以太伤害加成"})
            else:
                value = value.group(1)
                value = 转浮点数(value)
                角色库[键].update({角色属性[i]: value})
            i += 1
        js = json.loads(jso['data']['page']['modules'][8]['components'][0]['data'])
        j = js['tables'][0]['row'][0][1]
        soup = BeautifulSoup(j, 'html.parser')
        sou = soup.select("p>span")[0].text
        so = sou.split('提升', maxsplit=1)
        s = so[0]
        if s == '异常掌控':
            s = '基础异常掌控'
        角色库[键].update({'核心技属性': s})
        角色库[键].update({'核心技值': 转浮点数(so[1])})
        print(f'{键}属性已更新')

def 更新驱动盘库():
    for 键,值 in 驱动盘和id.items():
        驱动盘库[键] = {}
        params = {
            'app_sn': 'zzz_wiki',
            'entry_page_id': 值,
            'lang': 'zh-cn',
        }
        response = requests.get('https://api-takumi-static.mihoyo.com/hoyowiki/zzz/wapi/entry_page', params=params, headers=headers)
        jso = response.json()
        js = json.loads(jso['data']['page']['modules'][1]['components'][0]['data'])
        属性 = js['tables'][0]['row'][0][0]
        属性 = re.sub(r'<.*?>', '', 属性)
        属性 = 属性.rstrip('。')
        属性 = 属性.rstrip('点')
        属性和值 = 属性.split('＋', 1)
        if len(属性和值) < 2:
            属性和值 = 属性.split('+', 1)
        输出值 = 转浮点数(属性和值[1])

        if 属性和值[0] == "以太伤害":
            输出属性 = "以太伤害加成"
        elif 属性和值[0] == "火属性伤害":
            输出属性 = "火属性伤害加成"
        elif 属性和值[0] == "冰属性伤害":
            输出属性 = "冰属性伤害加成"
        elif 属性和值[0] == "电属性伤害":
            输出属性 = "电属性伤害加成"
        elif 属性和值[0] == "物理伤害":
            输出属性 = "物理伤害加成"
        else:
            输出属性 = 属性和值[0]

        驱动盘库[键].update({输出属性 : 输出值})
        print(f'{键}属性已更新')
    print('音擎库已更新完成')

def main():
    更新音擎库()
    更新角色库()
    更新驱动盘库()
    保存字典(音擎库, "音擎库")
    保存字典(角色库, "角色库")
    保存字典(驱动盘库, "驱动盘套装库")
    print('更新已完成,请关闭命令行窗口继续操作')

main()