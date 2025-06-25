using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    /// <summary>
    /// Draws a quad step-by-step using the Vector API and restarts after a delay.
    /// </summary>
    [UxmlElement]
    public partial class StepByStepQuadVectorElement : VisualElement
    {
        [UxmlAttribute] public float LineWidth { get; set; }
        [UxmlAttribute] public Color StrokeColor { get; set; }
        [UxmlAttribute] public Color FillColor { get; set; }
        [UxmlAttribute] public float StepDelaySeconds { get; set; } = 0.75f;
        [UxmlAttribute] public float RestartDelaySeconds { get; set; } = 1.5f;

        int m_CurrentStep = 0;
        float m_Timer = 0f;

        const int k_TotalSteps = 6; // 0–6
        Vector2[] m_Points;

        public StepByStepQuadVectorElement()
        {
            LineWidth = 10f;
            StrokeColor = Color.white;
            FillColor = new Color32(0, 127, 217, 255);
            StepDelaySeconds = 0.75f;
            RestartDelaySeconds = 1.5f;

            generateVisualContent += OnGenerateVisualContent;

            // Step updater
            schedule.Execute(UpdateStep).Every(16); // ~60fps
        }

        void UpdateStep()
        {
            m_Timer += Time.deltaTime;

            // If complete, pause longer before restarting
            if (m_CurrentStep >= k_TotalSteps)
            {
                if (m_Timer >= RestartDelaySeconds)
                {
                    m_CurrentStep = 0;
                    m_Timer = 0f;
                    MarkDirtyRepaint();
                }
                return;
            }

            // Otherwise, increment through each drawing step
            if (m_Timer >= StepDelaySeconds)
            {
                m_CurrentStep++;
                m_Timer = 0f;
                MarkDirtyRepaint();
            }
        }

        void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            if (layout.width == 0 || layout.height == 0)
                return;

            if (m_Points == null || m_Points.Length != 4)
            {
                m_Points = new Vector2[]
                {
                    new Vector2(0, 0),
                    new Vector2(layout.width, 0),
                    new Vector2(layout.width, layout.height),
                    new Vector2(0, layout.height)
                };
            }

            var painter = ctx.painter2D;
            painter.strokeColor = StrokeColor;
            painter.lineWidth = LineWidth;

            // Step 0: Draw nothing
            if (m_CurrentStep < 1)
                return;

            // Step 1-4: Begin and draw each segment one by one;  extra call to Stroke here for visualization
            // Make fill transparent
            for (int i = 1; i <= 3 && i < m_CurrentStep; i++)
            {
                painter.BeginPath();
                painter.fillColor =  new Color32(0, 127, 217, 0);
                painter.Fill();
                painter.MoveTo(m_Points[i - 1]);
                painter.LineTo(m_Points[i]);
                painter.lineCap = LineCap.Round;
                painter.Stroke();

            }

            // Step 5: Close the path from P3 to P0
            if (m_CurrentStep == 5)
            {
                painter.BeginPath();
                painter.fillColor =  new Color32(0, 127, 217, 0);
                painter.Fill();
                painter.MoveTo(m_Points[3]);
                painter.LineTo(m_Points[0]);
                painter.Stroke();
            }

            // Step 6: Fill the shape
            if (m_CurrentStep >= 6)
            {
                painter.BeginPath();
                painter.MoveTo(m_Points[0]);
                painter.LineTo(m_Points[1]);
                painter.LineTo(m_Points[2]);
                painter.LineTo(m_Points[3]);
                painter.ClosePath();

                painter.fillColor = FillColor;
                painter.Fill();
                painter.Stroke();
            }
            
        }
    }
}