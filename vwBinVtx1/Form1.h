#pragma once

namespace vwBinVtx1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Form1 �̊T�v
	///
	/// �x��: ���̃N���X�̖��O��ύX����ꍇ�A���̃N���X���ˑ����邷�ׂĂ� .resx �t�@�C���Ɋ֘A�t����ꂽ
	///          �}�l�[�W ���\�[�X �R���p�C�� �c�[���ɑ΂��� 'Resource File Name' �v���p�e�B��
	///          �ύX����K�v������܂��B���̕ύX���s��Ȃ��ƁA
	///          �f�U�C�i�ƁA���̃t�H�[���Ɋ֘A�t����ꂽ���[�J���C�Y�ς݃��\�[�X�Ƃ��A
	///          ���������݂ɗ��p�ł��Ȃ��Ȃ�܂��B
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: �����ɃR���X�g���N�^ �R�[�h��ǉ����܂�
			//
		}

	protected:
		/// <summary>
		/// �g�p���̃��\�[�X�����ׂăN���[���A�b�v���܂��B
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^  label1;
	protected: 
	private: System::Windows::Forms::OpenFileDialog^  openFileDialog1;

	private:
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"MS Reference Sans Serif", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->label1->Location = System::Drawing::Point(12, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(46, 16);
			this->label1->TabIndex = 0;
			this->label1->Text = L"label1";
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"H:\\Proj\\khkh_xldM\\bin\\Debug\\bin\\F_TT525.04.f_tt.bin";
			this->openFileDialog1->Filter = L"*.bin|*.bin||";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(760, 401);
			this->Controls->Add(this->label1);
			this->Name = L"Form1";
			this->Text = L"vwBinVtx1";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

	private: System::Void Form1_Load(System::Object^ sender, System::EventArgs^ e) {
				 if (!File::Exists(openFileDialog1->FileName))
					 openFileDialog1->ShowDialog(this);
				 if (!File::Exists(openFileDialog1->FileName))
					 return;

				 openIt(openFileDialog1->FileName);
			 }

			void openIt(String^ f) {
				FileStream^ fs = File::OpenRead(f);
				try {
					BinaryReader^ br = gcnew BinaryReader(fs);
					String^ s = f + "\r\n\r\n";
					String^ fmt = "f3";
#if 0
#elif 0
					fs->Position = 0x2C8;
					int len1 = br->ReadInt32(); // @0x2C8
					int len2 = br->ReadInt32(); // @0x2CC
					int len3 = br->ReadInt32(); // @0x2D0
					int len4 = br->ReadInt32(); // @0x2D4

					fs->Position = 0x300;
					for (int t=0; t<len1; t++) {
						s += br->ReadSingle().ToString(fmt) + "�@";
					}
					s += "\r\n";

					for (int t=0; t<len2; t++) {
						s += br->ReadSingle().ToString(fmt) + "�@";
					}
					s += "\r\n";

#elif 1
					fs->Position = 0x350;

					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
#elif 0
					fs->Position = 0xD0;

					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += br->ReadSingle().ToString(fmt) + "�@";
					s += "\r\n";
					s += "\r\n";

					fs->Position = 0x100;
					s += br->ReadSingle().ToString() + "\r\n";
					s += "\r\n";

					for (int t=0; t<3; t++) {
						fs->Position = 0x200 + 0x40*t;
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + " �b ";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + " �b ";
						s += "\r\n";
					}
					s += "\r\n";

					fs->Position = 0x350;
					for (int t=0; t<6; t++) {
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + " �b ";
						s += "\r\n";
					}
					s += "\r\n";

					fs->Position = 0x4C0;
					for (int t=0; t<5; t++) {
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + "�@";
						s += br->ReadSingle().ToString(fmt) + " �b ";
						s += "\r\n";
					}
					s += "\r\n";
#endif

					label1->Text = s;
				}
				finally {
					fs->Close();
				}
			}
	};
}

