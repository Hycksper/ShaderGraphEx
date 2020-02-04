using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Graphing.Util;
using UnityEditor.ShaderGraph.Data;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEditor.UIElements;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace UnityEditor.ShaderGraph.Drawing
{
    class SimpleLitSettingsView : VisualElement
    {
        SimpleLitMasterNode m_Node;
        public SimpleLitSettingsView(SimpleLitMasterNode node)
        {
            m_Node = node;

            PropertySheet ps = new PropertySheet();

            ps.Add(new PropertyRow(new Label("Surface")), (row) =>
                {
                    row.Add(new EnumField(SurfaceType.Opaque), (field) =>
                    {
                        field.value = m_Node.surfaceType;
                        field.RegisterValueChangedCallback(ChangeSurface);
                    });
                });
            
            ps.Add(new PropertyRow(new Label("RenderQueue")), (row) =>
            {
                row.Add(new EnumField(SurfaceMaterialTags.RenderQueue.Geometry), (field) =>
                {
                    field.value = m_Node.surfaceType;
                    field.RegisterValueChangedCallback(ChangeRenderQueue);
                });
            });
            
            ps.Add(new PropertyRow(new Label("ZTest")), (row) =>
            {
                row.Add(new EnumField(ZTest.LEqual), (field) =>
                {
                    field.value = m_Node.zTest;
                    field.RegisterValueChangedCallback(ChangeZTest);
                });
            });
            
            ps.Add(new PropertyRow(new Label("ZWrite")), (row) =>
            {
                row.Add(new EnumField(ZWrite.On), (field) =>
                {
                    field.value = m_Node.zWrite;
                    field.RegisterValueChangedCallback(ChangeZWrite);
                });
            });

            ps.Add(new PropertyRow(new Label("Blend")), (row) =>
                {
                    row.Add(new EnumField(AlphaMode.Additive), (field) =>
                    {
                        field.value = m_Node.alphaMode;
                        field.RegisterValueChangedCallback(ChangeAlphaMode);
                    });
                });

            ps.Add(new PropertyRow(new Label("Two Sided")), (row) =>
                {
                    row.Add(new Toggle(), (toggle) =>
                    {
                        toggle.value = m_Node.twoSided.isOn;
                        toggle.OnToggleChanged(ChangeTwoSided);
                    });
                });

            Add(ps);
        }
        
        void ChangeZTest(ChangeEvent<Enum> evt)
        {
            if (Equals(m_Node.zTest, evt.newValue))
                return;

            m_Node.owner.owner.RegisterCompleteObjectUndo("ZTest Change");
            m_Node.zTest = (ZTest)evt.newValue;
        }
        
        void ChangeZWrite(ChangeEvent<Enum> evt)
        {
            if (Equals(m_Node.zWrite, evt.newValue))
                return;

            m_Node.owner.owner.RegisterCompleteObjectUndo("ZWrite Change");
            m_Node.zWrite = (ZWrite)evt.newValue;
        }
        
        void ChangeSurface(ChangeEvent<Enum> evt)
        {
            if (Equals(m_Node.surfaceType, evt.newValue))
                return;

            m_Node.owner.owner.RegisterCompleteObjectUndo("Surface Change");
            m_Node.surfaceType = (SurfaceType)evt.newValue;
        }

        void ChangeAlphaMode(ChangeEvent<Enum> evt)
        {
            if (Equals(m_Node.alphaMode, evt.newValue))
                return;

            m_Node.owner.owner.RegisterCompleteObjectUndo("Alpha Mode Change");
            m_Node.alphaMode = (AlphaMode)evt.newValue;
        }

        void ChangeTwoSided(ChangeEvent<bool> evt)
        {
            m_Node.owner.owner.RegisterCompleteObjectUndo("Two Sided Change");
            ToggleData td = m_Node.twoSided;
            td.isOn = evt.newValue;
            m_Node.twoSided = td;
        }
        
        void ChangeRenderQueue(ChangeEvent<Enum> evt)
        {
            m_Node.owner.owner.RegisterCompleteObjectUndo("RenderQueue Change");
            m_Node.renderQueue = (SurfaceMaterialTags.RenderQueue)evt.newValue;
        }
    }
}
