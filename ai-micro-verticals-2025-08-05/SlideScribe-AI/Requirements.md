---
title: SlideScribe AI — 需求规格说明（Requirements）
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 范围与目标

- 长文本/转录 → 8–15 页演示；包含主题、配图建议、讲稿。
- 宿主插件原地生成与插入。

## 用户旅程

| 步骤 | 行为 | 产出 |
|---|---|---|
| 导入 | 粘贴/上传/转录 | 大纲 |
| 生成 | 选择主题与页数 | 页面与讲稿 |
| 导出 | PPTX/Slides/Notion | 成品演示 |

## 功能需求

- [ ] 解析与大纲抽取
- [ ] 主题/母版与版式
- [ ] Speaker Notes 自动生成
- [ ] 配图与图标建议（可选填充）
- [ ] 导出 PPTX/Slides/Notion
- [ ] PowerPoint/Slides 插件
- [ ] 支付/额度/退款

## 非功能

- 性能：10 页 ≤ 90 秒；P95 ≤ 150 秒。
- 隐私：内容不被训练；可删除。

## 数据模型

| 实体 | 字段 |
|---|---|
| User | id,email,plan |
| Deck | id,userId,pages,theme |
| Job | id,deckId,status,tokens,latency |

## 外部集成

| 集成 | 方式 | 审核 | 分成 |
|---|---|---|---|
| PowerPoint | Office JavaScript API | 3–5 天 | 0% |
| Google Slides | Apps Script + Slides API | 3–7 天 | 0% |
| Stripe/Paddle | Checkout + Webhook | 即时/1–2 天 | - |

## 验收

| 项 | 标准 |
|---|---|
| 保真 | 样式一致性 ≥ 95% |
| 性能 | P95 ≤ 150 秒 |
| 稳定 | 24h 错误率 ≤ 1% |

