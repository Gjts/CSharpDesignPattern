---
title: SlideScribe AI — 长文/会议纪要一键变 10 页高转化演示（2C 微垂直）
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 概述

- 目标：将长文、会议纪要、研究报告在 5–10 分钟内转为 8–15 页结构化幻灯片，包含视觉排版与演讲备注，减少 90% 制作时间。
- 上线目标（30 天）：MRR ≥ USD 1,800；单次转化 ≥ 12%。

## 目标用户与场景

| 人群 | 场景 | 价值 |
|---|---|---|
| 学生/求职者 | 课程汇报/作品展示 | 快速成稿，模板规范 |
| 创业者 | 路演材料/进度汇报 | 清晰结构与讲述逻辑 |
| 顾问/产品经理 | 需求评审/复盘 | 节省排版时间与对齐风格 |

## 用户故事

- 作为用户，我希望粘贴文档或上传音频/视频转录，一键生成结构化 PPT。
- 作为用户，我希望选择主题与风格，并自动生成演讲备注。
- 作为用户，我希望差异高亮、保留原图表与链接。

## 功能范围

| 模块 | 功能 | 说明 |
|---|---|---|
| 内容解析 | 文本/转录解析、大纲抽取 | 识别层级与要点 |
| 幻灯片生成 | 页面结构、主题与版式 | 自动配图与图标建议 |
| 备注与讲稿 | 每页 Speaker Notes | 30–60 秒口播长度 |
| 导出 | PPTX/Google Slides/Notion | 保留母版与样式 |
| 事实保护 | URL/数字/表格锁定 | 防丢失与混改 |

## 功能优化版（PowerPoint 与 Google Slides 插件）

- 名称：SlideScribe for PowerPoint / Slides
- 目标：在宿主中 0 学习成本完成从选中文段→生成页面→插入母版，效率 ≥10×。

| 平台 | 接入方式 | 审核周期 | 分成 |
|---|---|---|---|
| PowerPoint | Office JavaScript API（PowerPoint）+ 清单部署 + AppSource | 3–5 天 | 0% |
| Google Slides | Apps Script + Slides Service + Google Workspace Marketplace | 3–7 天 | 0% |

流程（Mermaid）：

```mermaid
flowchart LR
U[选中文段/上传文档] --> P[侧栏生成大纲]
P --> L[生成页面与母版]
L --> I[一键插入到当前演示]
I --> N[生成 Speaker Notes]
```

## 成功指标

| 指标 | 目标 |
|---|---|
| 首次生成到导出用时 | ≤ 10 分钟 |
| 试用→付费 | ≥ 12% |
| 复用率（次月） | ≥ 40% |

## 非功能需求

- 性能：10 页演示 ≤ 90 秒生成；P95 ≤ 150 秒。
- 保真：PPTX 格式与母版一致性 ≥ 95%。
- 隐私：不训练用户内容；可一键清除。

## 技术与部署

| 层级 | 选型 |
|---|---|
| 前端 | Next.js 14 |
| 后端 | .NET 8 Minimal API + PostgreSQL |
| AI | GPT-4-turbo |
| 支付 | Stripe + Paddle |
| 部署 | Railway + Cloudflare Pages |

## 里程碑（30 天）

- [ ] D1–D3：解析与大纲 MVP
- [ ] D4–D8：PPTX 生成与母版
- [ ] D9–D12：PowerPoint 插件 MVP + 提交
- [ ] D13–D16：Google Slides 插件 MVP + 提交
- [ ] D17–D24：讲稿/备注与主题库
- [ ] D25–D30：增长与计费优化

