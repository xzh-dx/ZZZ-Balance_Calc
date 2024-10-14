@echo off
REM 获取批处理文件所在目录并切换到该目录
cd /d %~dp0

REM 设置pip使用清华源
set PIP_INDEX_URL=https://pypi.tuna.tsinghua.edu.cn/simple

REM 使用python3.8的pip安装requirements.txt中的所有库
.\python3.8\python.exe -m pip install -r requirements.txt

REM 暂停，以便查看安装输出
pause