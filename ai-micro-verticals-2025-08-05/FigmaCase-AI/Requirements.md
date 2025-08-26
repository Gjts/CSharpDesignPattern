---
title: FigmaCase AI — 需求规格说明（Requirements）
date: 2025-08-05
author: 独立开发者
version: v1.0.0
---

## 范围与目标

- 解析设计/产品材料 → 生成案例页面 → 导出到 Figma/Notion/静态站。

## 用户旅程

| 步骤 | 行为 | 产出 |
|---|---|---|
| 导入 | Figma/Notion/GitHub 链接 | 材料与截图 |
| 生成 | 选择框架与主题 | 案例页面 |
| 导出 | Figma/Notion/HTML | 成品 |

## 功能需求

- [ ] 解析与截图抽取
- [ ] STAR/CASE 框架生成
- [ ] 指标与图表模块
- [ ] Figma 插件与页面导出
- [ ] 支付与额度

## 非功能

- 性能：生成 ≤ 5 分钟；P95 ≤ 8 分钟。
- 保真：Figma 页面样式一致性 ≥ 95%。

## 外部集成

| 集成 | 方式 | 审核 | 分成 |
|---|---|---|---|
| Figma | Figma Plugin API | 1–2 天 | 0% |
| Notion | OAuth + API | 无需 | 0% |
| Stripe/Paddle | Checkout + Webhook | 即时/1–2 天 | - |

