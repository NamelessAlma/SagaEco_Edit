VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "��ͼ����������"
   ClientHeight    =   6975
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   13410
   ForeColor       =   &H80000010&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   6975
   ScaleWidth      =   13410
   StartUpPosition =   1  '����������
   Begin VB.Frame Frame4 
      Caption         =   "�����ο�"
      Height          =   6855
      Left            =   7440
      TabIndex        =   22
      Top             =   120
      Width           =   5895
      Begin VB.CommandButton Command3 
         Caption         =   "����ѡ�����ݵ�Ŀ���ͼ"
         Height          =   375
         Left            =   3360
         TabIndex        =   26
         Top             =   240
         Width           =   2415
      End
      Begin VB.ListBox List2 
         Height          =   6000
         Left            =   120
         TabIndex        =   25
         Top             =   720
         Width           =   5655
      End
      Begin VB.TextBox Text7 
         Height          =   270
         Left            =   1200
         TabIndex        =   24
         Top             =   300
         Width           =   2055
      End
      Begin VB.Label Label7 
         Caption         =   "��ѯ��ͼID:"
         Height          =   255
         Left            =   120
         TabIndex        =   23
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "��������"
      ForeColor       =   &H00FF00FF&
      Height          =   2175
      Left            =   120
      TabIndex        =   16
      Top             =   4800
      Width           =   7215
      Begin VB.TextBox c 
         Appearance      =   0  'Flat
         Height          =   1815
         Left            =   120
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   17
         Top             =   240
         Width           =   6975
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "��ͼ�б�"
      ForeColor       =   &H00FF0000&
      Height          =   2415
      Left            =   120
      TabIndex        =   14
      Top             =   2280
      Width           =   7215
      Begin VB.ListBox List1 
         Height          =   2040
         Left            =   120
         TabIndex        =   15
         Top             =   240
         Width           =   6975
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "��ͼ������Ϣ"
      BeginProperty Font 
         Name            =   "����"
         Size            =   9
         Charset         =   134
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   2055
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   7215
      Begin VB.CommandButton Command7 
         Caption         =   "���ID"
         Height          =   375
         Left            =   4920
         TabIndex        =   27
         Top             =   1560
         Width           =   975
      End
      Begin VB.CommandButton Command5 
         Caption         =   "����"
         Height          =   375
         Left            =   3720
         TabIndex        =   18
         Top             =   1560
         Width           =   1000
      End
      Begin VB.CommandButton Command4 
         Caption         =   "����"
         Height          =   375
         Left            =   2640
         TabIndex        =   13
         Top             =   1560
         Width           =   1000
      End
      Begin VB.TextBox Text6 
         Height          =   270
         Left            =   5280
         TabIndex        =   6
         Top             =   1020
         Width           =   1800
      End
      Begin VB.TextBox Text5 
         Height          =   270
         Left            =   1440
         TabIndex        =   5
         Top             =   1020
         Width           =   2280
      End
      Begin VB.CommandButton Command6 
         Caption         =   "����"
         Height          =   375
         Left            =   6120
         TabIndex        =   19
         Top             =   1560
         Width           =   1000
      End
      Begin VB.CommandButton Command2 
         Caption         =   "ɾ��ѡ��"
         Height          =   375
         Left            =   1320
         TabIndex        =   12
         Top             =   1560
         Width           =   1000
      End
      Begin VB.CommandButton Command1 
         Caption         =   "���"
         BeginProperty Font 
            Name            =   "����"
            Size            =   9
            Charset         =   134
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   120
         MaskColor       =   &H0080C0FF&
         TabIndex        =   11
         Top             =   1560
         Width           =   1000
      End
      Begin VB.TextBox Text4 
         Height          =   270
         Left            =   5040
         TabIndex        =   4
         Top             =   660
         Width           =   2055
      End
      Begin VB.TextBox Text3 
         Height          =   270
         Left            =   5040
         TabIndex        =   2
         Top             =   300
         Width           =   2055
      End
      Begin VB.TextBox Text2 
         Height          =   270
         Left            =   1320
         TabIndex        =   3
         Top             =   660
         Width           =   2415
      End
      Begin VB.TextBox Text1 
         Height          =   270
         Left            =   1320
         TabIndex        =   1
         Top             =   300
         Width           =   2415
      End
      Begin VB.Label Label6 
         Caption         =   "Ŀ���ͼX����:"
         Height          =   255
         Left            =   3840
         TabIndex        =   21
         Top             =   1080
         Width           =   1455
      End
      Begin VB.Label Label5 
         Caption         =   "Ŀ���ͼX����:"
         Height          =   255
         Left            =   120
         TabIndex        =   20
         Top             =   1080
         Width           =   1455
      End
      Begin VB.Label Label4 
         Caption         =   "EvenID:"
         Height          =   255
         Left            =   3840
         TabIndex        =   10
         Top             =   720
         Width           =   1215
      End
      Begin VB.Label Label3 
         Caption         =   "Ŀ�ĵ�ͼ���:"
         Height          =   255
         Left            =   3840
         TabIndex        =   9
         Top             =   360
         Width           =   1575
      End
      Begin VB.Label Label2 
         Caption         =   "Ŀ�ĵ�ͼ����:"
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   720
         Width           =   1455
      End
      Begin VB.Label Label1 
         Caption         =   "���͵�ͼ����:"
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   360
         Width           =   1335
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim data() As String

Private Sub c_GotFocus()
c.SelStart = 0
c.SelLength = Len(c.Text)
End Sub

Private Sub Command1_Click()
If Len(Text1.Text) > 0 And Len(Text2.Text) > 0 And Len(Text3.Text) > 0 And Len(Text4.Text) > 0 And Len(Text5.Text) > 0 And Len(Text6.Text) > 0 Then
If cheakevenid = True Then
List1.AddItem (Text1.Text & "," & Text2.Text & "," & Text3.Text & "," & Text4.Text & "," & Text5.Text & "," & Text6.Text)
End If

Else
MsgBox "������д����"
End If
End Sub

Private Sub Command2_Click()
If List1.ListIndex >= 0 Then List1.RemoveItem List1.ListIndex
End Sub

Private Sub Command3_Click()
If List2.ListIndex <> -1 Then
Dim tmp() As String
tmp = Split(List2.List(List2.ListIndex), ",")
Text2.Text = tmp(2)
Text3.Text = tmp(1)
End If
End Sub

Private Sub Command4_Click()
Dim tmp As String
For i = 0 To List1.ListCount - 1
tmp = tmp & List1.List(i) & vbCrLf
Next
    Open App.Path & "\map.csv" For Output As #1
    Print #1, tmp
    Close #1
MsgBox "����ɹ�"
End Sub

Private Sub Command5_Click()
On Error GoTo ex
Dim tmp As String
Dim tmp2() As String
tmp = OpenFile(App.Path & "\map.csv")

tmp2 = Split(tmp, vbCrLf)
List1.Clear
For i = 0 To UBound(tmp2) - 2
List1.AddItem tmp2(i)
Next
MsgBox "��ȡ�ɹ�"
ex:
End Sub

Private Sub Command6_Click()
Dim tmp As String
Dim tmp2() As String
c.Text = ""
For i = 0 To List1.ListCount - 1
tmp2 = Split(List1.List(i), ",")
tmp = tmp & "    public class P" & tmp2(3) & " : Portal { public P" & tmp2(3) & "() { Init(" & tmp2(3) & "," & tmp2(2) & "," & tmp2(4) & "," & tmp2(5) & ");} }" & "//ԭʼ��ͼ:" & tmp2(0) & " Ŀ���ͼ[" & tmp2(2) & "]:" & tmp2(1) & " Ŀ������:" & tmp2(4) & "," & tmp2(5) & vbCrLf


Next

            Open App.Path & "\d.txt" For Output As #1
        Print #1, tmp
        Close #1

c.Text = tmp
End Sub

Private Sub Command7_Click()
cheakevenid
End Sub
Function cheakevenid() As Boolean
cheakevenid = True
For i = 0 To List1.ListCount - 1
If Split(List1.List(i), ",")(3) = Text4.Text Then
MsgBox "Eventid�Ѿ�����"
cheakevenid = False
End If
Next
End Function
Private Sub Form_Load()
On Error GoTo ex
Dim tmp As String
Dim tmp2() As String
tmp = OpenFile(App.Path & "\map_data.csv")
tmp2 = Split(tmp, vbCrLf)
List2.Clear
ReDim data(UBound(tmp2) - 1)
For i = 0 To UBound(tmp2) - 1
data(i) = tmp2(i)
List2.AddItem tmp2(i)
Next
ex:
End Sub


Private Sub List2_DblClick()
If List2.ListIndex <> -1 Then
Dim tmp() As String
tmp = Split(List2.List(List2.ListIndex), ",")
Text1.Text = tmp(2)
Text4.Text = tmp(0)
End If
End Sub

Private Sub Text1_GotFocus()
Text1.SelStart = 0
Text1.SelLength = Len(Text1.Text)
End Sub

Private Sub Text2_GotFocus()
Text2.SelStart = 0
Text2.SelLength = Len(Text2.Text)
End Sub

Private Sub Text3_GotFocus()
Text3.SelStart = 0
Text3.SelLength = Len(Text3.Text)
End Sub

Private Sub Text4_GotFocus()
Text4.SelStart = 0
Text4.SelLength = Len(Text4.Text)
End Sub

Private Sub Text5_GotFocus()
Text5.SelStart = 0
Text5.SelLength = Len(Text5.Text)
End Sub

Private Sub Text6_GotFocus()
Text6.SelStart = 0
Text6.SelLength = Len(Text6.Text)
End Sub

Private Sub Text7_Change()
List2.Clear
For Each skey In data
If InStr(1, Split(skey, ",")(1), Text7.Text) Then
List2.AddItem skey
End If
Next
End Sub
